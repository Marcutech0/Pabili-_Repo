using UnityEngine;
using System.Linq;
using TMPro;

public class CustomerAI : MonoBehaviour
{
    [Header("Dialogue")]
    public CustomerDialogue dialogue;
    public DialogueManager dialogueManager;

    [Header("Payment Settings")]
    public int maxExtraPayment = 5;

    public ProductData[] desiredProducts;
    public int moneyGiven;
    public bool isServed;

    public TextMeshProUGUI requestTextUI;

    private void Start()
    {
         ShowGreeting();
    }

    public void RequestProduct(ProductData product)
    {
        if (product == null)
        {
            Debug.LogError("Cannot request null product!");
            return;
        }

        desiredProducts = new ProductData[] { product };
        moneyGiven = Mathf.Max(
            product.productPrice,
            product.productPrice + Random.Range(0, maxExtraPayment + 1)
        );
        isServed = false;

        string productName = product.productName;
        string line = $"Pabili nga ng {productName}!";

        requestTextUI.text = line;

        Debug.Log($"🧍 Customer wants: {productName} | Paid: ₱{moneyGiven:F2}");
    }

    public bool ReceiveProduct(ProductData product)
    {
        if (isServed) return false;

        bool isCorrect = desiredProducts.Contains(product);

        if (isCorrect)
        {
            isServed = true;
            ShowThankYou();
        }
        else
        {
            ShowWrongProduct();
        }

        return isCorrect;

    }

    private void ShowGreeting()
    {
        if (dialogue.greetings.Length > 0)
        {
            dialogueManager.ShowDialogue(GetRandom(dialogue.greetings));
        }
    }

    private void ShowThankYou()
    {
        if (dialogue.thankYous.Length > 0)
        {
            dialogueManager.ShowDialogue(GetRandom(dialogue.thankYous));
        }
    }

    private void ShowWrongProduct()
    {
        if (dialogue.wrongProductLines.Length > 0)
        {
            dialogueManager.ShowDialogue(GetRandom(dialogue.wrongProductLines));
        }
    }

    private string GetRandom(string[] lines)
    {
        return lines[Random.Range(0, lines.Length)];
    }
}
