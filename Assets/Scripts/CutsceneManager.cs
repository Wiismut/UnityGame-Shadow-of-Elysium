using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {

    public void LoadLevel()
    {
        if (Application.isPlaying)
        {
            SceneManager.LoadScene("Lvl1");
        }
        else
        {
            Debug.LogWarning("LoadLevel");
        }
    }



    //public float cutsceneDuration = 25f;

    //private void Start()
    //{
    //    StartCoroutine(PlayCutscene());
    //}

    //private IEnumerator PlayCutscene()
    //{
    //    yield return new WaitForSeconds(cutsceneDuration);
    //    SceneManager.LoadScene("Lvl1");
    //}
}
