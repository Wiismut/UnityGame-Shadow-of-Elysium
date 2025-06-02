using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {
    public Image Icon;

    public void UpdateSlot(bool active, Sprite sprite)
    {
        if (active)
        {
            Icon.sprite = sprite;
            Icon.enabled = true;
        }
        else
        {
            Icon.enabled = false;
        }
    }
}
