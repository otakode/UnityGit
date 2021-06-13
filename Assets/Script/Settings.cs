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
	public Slider SoundsSlider = null;
	public AudioMixer audioMixer = null;

	#endregion

	#region Fields

	public SettingsBool Fullscreen { get; set; } = new SettingsBool("fullscreen", false);

	public SettingsFloat Music { get; set; } = new SettingsFloat("music", 100f);
	public SettingsFloat Sounds { get; set; } = new SettingsFloat("sounds", 100f);

	#endregion

	#region UnityEvents

	void Start()
	{
		if (FullscreenToggle)
		{
			FullscreenToggle.isOn = Fullscreen.Value;
			FullscreenToggle.onValueChanged.AddListener(SetFullscreen);
		}
		SetFullscreen(Fullscreen.Value);

		if (MusicSlider)
		{
			MusicSlider.value = Music.Value;
			MusicSlider.onValueChanged.AddListener(SetMusicVolume);
		}
		SetMusicVolume(Music.Value);

		if (SoundsSlider)
		{
			SoundsSlider.value = Sounds.Value;
			SoundsSlider.onValueChanged.AddListener(SetSoundsVolume);
		}
		SetSoundsVolume(Sounds.Value);
	}

	#endregion

	#region Methods

	public void SetFullscreen(bool state)
	{
		Fullscreen.Value = state; 
		Screen.fullScreen = state;
	}

	public void SetMusicVolume(float volume)
	{
		Music.Value = volume;
		audioMixer.SetFloat("MusicVolume", PercentToDecibels(volume));
	}

	public void SetSoundsVolume(float volume)
	{
		Sounds.Value = volume;
		audioMixer.SetFloat("SoundsVolume", PercentToDecibels(volume));
	}

	public static float PercentToDecibels(float percent)
	{
		return Mathf.Max(-80f, Mathf.Log10(percent / 100f) * 20f);
	}

	#endregion
}
