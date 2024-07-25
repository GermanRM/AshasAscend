using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public Button muteButton; // Button for mute toggle
    public Toggle muteCheckbox; // Checkbox to show mute state
    public AudioMixer audioMixer; // Reference to the AudioMixer

    private bool isMuted = false; // Track mute state
    private bool isUpdatingMuteState = false; // To prevent recursive event triggering

    void Start()
    {
        // Initialize the slider with the current volume
        float volume;
        audioMixer.GetFloat("Volume", out volume);
        volumeSlider.value = Mathf.Pow(10, volume / 20); // Convert dB to linear

        // Add listeners for the slider, mute button, and checkbox
        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteButton.onClick.AddListener(ToggleMute);
        muteCheckbox.onValueChanged.AddListener(OnCheckboxValueChanged);

        // Initialize the mute button state
        UpdateMuteState();
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
        if (isUpdatingMuteState)
            return;

        isMuted = !isMuted;
        ApplyMuteState();
    }

    private void OnCheckboxValueChanged(bool isOn)
    {
        if (isUpdatingMuteState)
            return;

        isMuted = isOn;
        ApplyMuteState();
    }

    private void ApplyMuteState()
    {
        isUpdatingMuteState = true;

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

        UpdateMuteState();
        isUpdatingMuteState = false;
    }

    private void UpdateMuteState()
    {
        if (muteCheckbox != null)
        {
            muteCheckbox.isOn = isMuted;
        }
    }
}
