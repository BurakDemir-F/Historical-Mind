using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapTest : MonoBehaviour
{
    [SerializeField] private GameObject miniMap;

    public void MakeChildren()
    {
        for (int i = 0; i < 99; i++)
        {
            var image = new GameObject();
            var imag = image.AddComponent<Image>();
            imag.color = GetRandomColor();
            image.transform.SetParent(miniMap.transform);
        }
    }

    public void DeleteAllChildren()
    {
        for (int i = 0; i < miniMap.transform.childCount; i++)
        {
            var child = miniMap.transform.GetChild(i);
            Destroy(child.GetComponent<Image>());
            DestroyImmediate(child);
        }
    }

    private Color GetRandomColor()
    {
        var randomValue = Random.Range(0, 255) / 255f;
        return new Color(randomValue, randomValue, randomValue, randomValue);
    }
    
}