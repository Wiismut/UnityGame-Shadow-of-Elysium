using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            int selectedPlayer = PlayerPrefs.GetInt("SelectPlayer", 0);
            string lossSceneName = selectedPlayer == 0 ? "GoodEndMale" : "GoodEndFemale";
            SceneManager.LoadScene(lossSceneName);
        }
    }
}