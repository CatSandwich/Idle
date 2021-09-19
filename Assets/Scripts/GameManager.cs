using System;
using UI.Settlement;
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
            if (value == _action) return;
            _action = value;
            ActionChanged();
        }
    }
    private Actions _action = Actions.Idle;
    public event Action ActionChanged = () => { };

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
        var delta = Stats.ActionResourceDeltas[Action];
        delta += new ResourceDelta
        {
            Food = Stats.Resources[Stats.Resource.Population].Value
        };
        if ((delta + Stats.ToDelta()).HasNegative())
        {
            Debug.Log("Next day would make negative value, skipping.");
            return;
        }
        Stats.ApplyDelta(delta);
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
