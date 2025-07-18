using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProductLoader : MonoBehaviour
{
    public List<GameObject> productPrefabs = new();

#if UNITY_EDITOR
    void OnValidate()
    {
        LoadProductPrefabs();
    }

    [ContextMenu("Load Product Prefabs")]
    public void LoadProductPrefabs()
    {
        productPrefabs.Clear();

        // Find all prefabs in the Prefabs folder
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Prefabs" });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab.GetComponent<ProductControls>() != null)
            {
                productPrefabs.Add(prefab);
            }
        }

        Debug.Log($"Found {productPrefabs.Count} product prefabs");
    }
#endif
}