using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {
    public LevelTrigger lvlEnter;
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.transform.tag == "Player")
        {
            lvlEnter.FadeToLevel();
        }
    }
}
