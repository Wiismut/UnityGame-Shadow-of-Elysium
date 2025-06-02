using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }
    private Slider volumeSlider;
    [Range(0f, 1f)] public float musicVolume = 1f;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadVolumeSettings();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndBindSlider();
    }

    private void Start()
    {
        FindAndBindSlider();
    }
    public void SetVolumeSlider(Slider slider)
    {
        volumeSlider = slider;
        FindAndBindSlider();
    }

    private void FindAndBindSlider()
    {
        if (volumeSlider == null)
        {
            return;
        }

        volumeSlider.value = musicVolume;
        volumeSlider.onValueChanged.RemoveAllListeners();
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    public void UpdateVolume(float value)
    {
        musicVolume = value;
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();

        MusicBackground[] musicSources = FindObjectsOfType<MusicBackground>();
        foreach (MusicBackground music in musicSources)
        {
            music.UpdateVolume();
        }
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
    }
}
