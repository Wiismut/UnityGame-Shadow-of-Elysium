using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour {
    private GameObject[] characters;
    private int index;
    public AudioClip selectSound;
    private AudioSource audioSource;
    [SerializeField] private float delayBeforeSceneLoad = 0.5f;

    private void Start()
    {
        index = PlayerPrefs.GetInt("SelectPlayer");

        characters = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characters[i] = transform.GetChild(i).gameObject;
        }
        foreach (GameObject go in characters)
        {
            go.SetActive(false);
        }
        if (characters[index])
        {
            characters[index].SetActive(true);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.clip = selectSound;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 0f;
        }
        else
        {
            Debug.LogError("звука нет");
        }

        if (selectSound == null)
        {
            Debug.LogError("звука нет");
        }

    }



    private void SelectSound()
    {
        if (audioSource != null && selectSound != null)
        {
            audioSource.PlayOneShot(selectSound);
        }
        else
        {
            Debug.LogError("звука нет");
        }
    }



    public void SelectLeft()
    {
        characters[index].SetActive(false);
        index--;
        if (index < 0)
        {
            index = characters.Length - 1;
        }
        characters[index].SetActive(true);
    }

    public void SelectRight()
    {
        characters[index].SetActive(false);
        index++;
        if (index == characters.Length)
        {
            index = 0;
        }
        characters[index].SetActive(true);
    }








    public void StartScene()
    {

        SelectSound();
        StartCoroutine(StartSceneDelay());
    }







    public IEnumerator StartSceneDelay()
    {
        yield return new WaitForSeconds(selectSound.length);
        PlayerPrefs.SetInt("SelectPlayer", index);
        string cutsceneSceneName = index == 0 ? "StartCutsceneMale" : "StartCutsceneFemale";
        SceneManager.LoadScene(cutsceneSceneName);

        
    }



}
