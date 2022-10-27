using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// </summary>
static public partial class PlayerPrefabs
{
    static public void SetPrefix(string prefix)
    {
        PlayerPrefabs.prefix = prefix;
    }
    static public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(GetKey(key));
    }
    static public bool DeleteKey(string key)
    {
        if (HasKey(key))
        {
            PlayerPrefs.DeleteKey(GetKey(key));
            return true;
        }
        return false;
    }
    static public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
/// <summary>
/// set.
/// </summary>
partial class PlayerPrefabs
{
    static public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(GetKey(key), value);
    }
    static public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(GetKey(key), value);
    }
    static public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(GetKey(key), value);
    }
    static public void SetBool(string key, bool value)
    {
        PlayerPrefs.SetString(GetKey(key), (value) ? True : False);
    }
    static public void EncodeInt(string key, int value)
    {
        PlayerPrefs.SetString(EncodeKey(key), EncodeInt32(value));
    }
    static public void EncodeFloat(string key, float value)
    {
        PlayerPrefs.SetString(EncodeKey(key), EncodeFloat(value));
    }
    static public void EncodeString(string key, string value)
    {
        PlayerPrefs.SetString(EncodeKey(key), EncodeString(value));
    }
    static public void EncodeBool(string key, bool value)
    {
        PlayerPrefs.SetString(EncodeKey(key), EncodeString((value) ? True : False));
    }
}
/// <summary>
/// get.
/// </summary>
partial class PlayerPrefabs
{
    static public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(GetKey(key));
    }
    static public int GetInt(string key, int defaultValue)
    {
        return PlayerPrefs.GetInt(GetKey(key), defaultValue);
    }
    static public float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(GetKey(key));
    }
    static public float GetFloat(string key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(GetKey(key), defaultValue);
    }
    static public string GetString(string key)
    {
        return PlayerPrefs.GetString(GetKey(key));
    }
    static public string GetString(string key, string defaultValue)
    {
        return PlayerPrefs.GetString(GetKey(key), defaultValue);
    }
    static public bool GetBool(string key)
    {
        return GetBool(key, false);
    }
    static public bool GetBool(string key, bool defaultValue)
    {
        return PlayerPrefs.GetString(GetKey(key), (defaultValue) ? True : False).Equals(True);
    }
    static public int DecodeInt(string key, int defaultValue)
    {
        string result = PlayerPrefs.GetString(EncodeKey(key), null);
        if (string.IsNullOrEmpty(result))
        {
            return defaultValue;
        }
        return DecodeInt32(result);
    }
    static public float DecodeFloat(string key, float defaultValue)
    {
        string result = PlayerPrefs.GetString(EncodeKey(key), null);
        if (string.IsNullOrEmpty(result))
        {
            return defaultValue;
        }
        return DecodeFloat(result);
    }
    static public string DecodeString(string key, string defaultValue)
    {
        string result = PlayerPrefs.GetString(EncodeKey(key), null);
        if (string.IsNullOrEmpty(result))
        {
            return defaultValue;
        }
        return DecodeString(result);
    }
    static public bool DecodeBool(string key, bool defaultValue)
    {
        string result = PlayerPrefs.GetString(EncodeKey(key), null);
        if (string.IsNullOrEmpty(result))
        {
            return defaultValue;
        }
        return DecodeString(result).Equals(True);
    }
}
/// <summary>
/// 
/// </summary>
partial class PlayerPrefabs
{
    static readonly string True = "True";
    static readonly string False = "False";

    static string prefix = null;

    static string GetKey(string key)
    {
        return prefix + key;
    }
    static string EncodeKey(string key)
    {
        return prefix + key;
    }
    static string EncodeInt32(int value)
    {
        return System.Convert.ToString(value);
    }
    static string EncodeFloat(float value)
    {
        return System.Convert.ToString(value);
    }
    static string EncodeString(string value)
    {
        return value;
    }
    static int DecodeInt32(string value)
    {
        return System.Convert.ToInt32(value);
    }
    static float DecodeFloat(string value)
    {
        return System.Convert.ToSingle(value);
    }
    static string DecodeString(string value)
    {
        return value;
    }
}
