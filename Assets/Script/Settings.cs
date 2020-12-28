using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	#region References

	[Header("Video")]
	public Toggle FullscreenToggle = null;

	[Header("Audio")]
	public Slider MusicSlider = null;
	public Slider SoundSlider = null;
	public AudioMixer audioMixer = null;

	#endregion

	#region Fields

	public SettingsBool Fullscreen { get; set; } = new SettingsBool("fullscreen", true);

	public SettingsFloat Music { get; set; } = new SettingsFloat("music", 100f);
	public SettingsFloat Sounds { get; set; } = new SettingsFloat("sounds", 100f);

	#endregion

	void Start()
	{
		if (FullscreenToggle)
		{
			FullscreenToggle.onValueChanged.AddListener((bool newValue) => { Fullscreen.Value = newValue; Screen.fullScreen = newValue; });
			FullscreenToggle.isOn = Fullscreen.Value;
		}

		if (MusicSlider)
		{
			MusicSlider.onValueChanged.AddListener((float newValue) => { Music.Value = newValue; audioMixer.SetFloat("MusicVolume", PercentToDecibels(newValue)); });
			MusicSlider.value = Music.Value;
		}

		if (SoundSlider)
		{
			SoundSlider.onValueChanged.AddListener((float newValue) => { Sounds.Value = newValue; audioMixer.SetFloat("SoundsVolume", PercentToDecibels(newValue)); });
			SoundSlider.value = Sounds.Value;
		}
	}

	public static float PercentToDecibels(float percent)
	{
		return Mathf.Max(-80f, Mathf.Log10(percent / 100f) * 20f);
	}
}
