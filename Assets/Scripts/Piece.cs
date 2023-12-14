using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Piece : MonoBehaviour
{
    public int x,y;
    public bool bomb;
    public GameObject imageObject;

    private void OnMouseDown()
    {
        if (bomb)
        {
            if (imageObject != null)
                imageObject.SetActive(true);
        }
        else
        {

            TextMeshProUGUI textComponent = GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
                textComponent.text = Generator.gen.GetBombsAround(x, y).ToString();
            else Debug.LogError("Text component not found in the hierarchy of Piece.");
        }
        
        
    }
}
