using UnityEngine;
using UnityEngine.UI;

public class VolumeSetup : MonoBehaviour {
    [SerializeField] private Slider slider;

    private void Start()
    {
        SoundManager.Instance.SetVolumeSlider(slider);
    }
}
