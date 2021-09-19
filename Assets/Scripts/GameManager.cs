using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance ??= FindObjectOfType<GameManager>();
    private static GameManager _instance;

    public bool IsRunning = true;
    public float DaySpeed = 0.1f;
    private float _percentToNextDay = 0f;
    
    public Actions Action
    {
        get => _action;
        set
        {
            _action = value;
            ActionChanged(value);
        }
    }
    private Actions _action = Actions.Idle;
    public event Action<Actions> ActionChanged = action => { };

    #region Resources
    [Header("Images")] 
    public Texture2D Food;
    public Texture2D Wood;
    public Texture2D Stone;
    public Texture2D BasicHouses;

    public Texture2D GetTexture(Stats.Resource resource) => resource switch
    {
        Stats.Resource.Food => Food,
        Stats.Resource.Wood => Wood,
        Stats.Resource.Stone => Stone,
        Stats.Resource.BasicHouses => BasicHouses,
        _ => throw new ArgumentOutOfRangeException(nameof(resource), resource, null)
    };
    #endregion
    
    void Update()
    {
        if (!IsRunning) return;
        _percentToNextDay += DaySpeed * Time.deltaTime;
        if (_trySubtract(ref _percentToNextDay, 1f)) NextDay();
    }

    public void Reset() => Stats.Reset();

    public void NextDay()
    {
        foreach (var stat in Stats.StatArray)
        {
            if (!stat.IsValid)
            {
                Debug.Log($"Insufficient {stat.Name}, day failed.");
                return;
            }
        }

        foreach (var stat in Stats.StatArray)
        {
            stat.NextDay();
        }
    }

    private bool _trySubtract(ref float val, float amount)
    {
        if (val < amount) return false;
        val -= amount;
        return true;
    }
    
    public enum Tabs
    {
        Home,
        Player,
        Settlement,
        Storage,
        Settings
    }

    public enum Actions
    {
        Idle,
        Fishing,
        Woodcutting,
        Mining
    }
}
