using UnityEngine;

[CreateAssetMenu(fileName = "CustomerDialogue", menuName = "NPC/Customer Dialogue")]
public class CustomerDialogue : ScriptableObject
{
    [TextArea(2, 4)]
    public string[] greetings;

    [TextArea(2, 4)]
    public string[] thankYous;

    [TextArea(2, 4)]
    public string[] wrongProductLines;

    [TextArea(2, 4)]
    public string[] idleLines;
}
