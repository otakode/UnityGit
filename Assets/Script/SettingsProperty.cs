using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SettingsProperty<T>
{
	public string playerPrefsKey;
	public T defaultValue;
	virtual public T Value { get; set; }

	public SettingsProperty(string key, T value)
	{
		playerPrefsKey = key;
		defaultValue = value;
	}
}

public class SettingsInt : SettingsProperty<int>
{
	public SettingsInt(string key, int value) : base(key, value) { }

	override public int Value
	{
		get { return PlayerPrefs.GetInt(playerPrefsKey, defaultValue); }
		set { PlayerPrefs.SetInt(playerPrefsKey, value); }
	}
}

public class SettingsBool : SettingsProperty<bool>
{
	public SettingsBool(string key, bool value) : base(key, value) { }

	override public bool Value
	{
		get { return PlayerPrefs.GetInt(playerPrefsKey, (defaultValue ? 1 : 0)) != 0; }
		set { PlayerPrefs.SetInt(playerPrefsKey, (value ? 1 : 0) ); }
	}
}

public class SettingsFloat : SettingsProperty<float>
{
	public SettingsFloat(string key, float value) : base(key, value) { }

	override public float Value
	{
		get { return PlayerPrefs.GetFloat(playerPrefsKey, defaultValue); }
		set { PlayerPrefs.SetFloat(playerPrefsKey, value); }
	}
}

public class SettingsString : SettingsProperty<string>
{
	public SettingsString(string key, string value) : base(key, value) { }

	override public string Value
	{
		get { return PlayerPrefs.GetString(playerPrefsKey, defaultValue); }
		set { PlayerPrefs.SetString(playerPrefsKey, value); }
	}
}
