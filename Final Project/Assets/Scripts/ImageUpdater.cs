using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageUpdater : MonoBehaviour
{
    public Image uiImage;
    public Sprite[] sprites; // Array of sprites for different ranges
    public int[] rangeStarts; // Array of range start values
    public int[] rangeEnds; // Array of range end values

    // Update the UI image based on the given gold count
    public void UpdateImage(int goldCount)
    {
        for (int i = 0; i < rangeStarts.Length; i++)
        {
            if (goldCount >= rangeStarts[i] && goldCount <= rangeEnds[i])
            {
                // Set the corresponding sprite based on the gold count range
                uiImage.sprite = sprites[i];
                return;
            }
        }
    }
}
