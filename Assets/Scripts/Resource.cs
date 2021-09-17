using System;
using UnityEngine;

[CreateAssetMenu]
public class Resource : ScriptableObject
{ 
    public string Name;
    public string Key;
    public int StartingValue;
    public Sprite Icon;

    public Action<int> ValueChanged;
    
    public int Value
    {
        get => PlayerPrefs.GetInt(Key, StartingValue);
        set
        {
            PlayerPrefs.SetInt(Key, value);
            ValueChanged.Invoke(value);
        }
    }

    public void Reset() => Value = StartingValue;
    public string ToString(bool label) => label ? $"{Name}: " : "" + Value;

    public enum Type
    {
        Food,
        Stone,
        Wood,
        BasicHouses
    }
}