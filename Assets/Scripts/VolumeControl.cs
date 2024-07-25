using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public Button volumeButton;
    public Button muteButton; // Button for mute toggle
    public Toggle muteCheckbox; // Checkbox to show mute state
    public GameObject volumeSliderUI; // Reference to the Slider UI GameObject
    public AudioMixer audioMixer; // Reference to the AudioMixer

    private bool isMuted = false; // Track mute state

    void Start()
    {
        // Initialize the slider with the current volume
        float volume;
        audioMixer.GetFloat("Volume", out volume);
        volumeSlider.value = Mathf.Pow(10, volume / 20); // Convert dB to linear

        // Add listeners for the slider and buttons
        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteButton.onClick.AddListener(ToggleMute);
        muteCheckbox.onValueChanged.AddListener(ToggleMuteFromCheckbox);

        // Initialize the mute button state
        UpdateMuteButton();
    }

    public void SetVolume(float sliderValue)
    {
        if (isMuted)
            return;

        // Convert the slider value to dB and set the volume in the AudioMixer
        float volume = Mathf.Lerp(-60f, 0f, sliderValue);
        audioMixer.SetFloat("Volume", volume);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            // Set the AudioMixer to mute
            audioMixer.SetFloat("Volume", -80f); // Use -80 dB or another low value to simulate mute
        }
        else
        {
            // Restore volume level based on slider value
            float sliderValue = volumeSlider.value;
            float volume = Mathf.Lerp(-60f, 0f, sliderValue);
            audioMixer.SetFloat("Volume", volume);
        }

        UpdateMuteButton();
    }

    public void ToggleMuteFromCheckbox(bool isChecked)
    {
        isMuted = isChecked;

        if (isMuted)
        {
            // Set the AudioMixer to mute
            audioMixer.SetFloat("Volume", -80f); // Use -80 dB or another low value to simulate mute
        }
        else
        {
            // Restore volume level based on slider value
            float sliderValue = volumeSlider.value;
            float volume = Mathf.Lerp(-60f, 0f, sliderValue);
            audioMixer.SetFloat("Volume", volume);
        }

        UpdateMuteButton();
    }

    private void UpdateMuteButton()
    {
        if (muteCheckbox != null)
        {
            muteCheckbox.isOn = isMuted;
        }
    }
}
