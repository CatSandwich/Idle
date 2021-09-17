using System;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    [Header("Tabs")]
    public GameObject Home;
    public GameObject Player;
    public GameObject Settlement;
    public GameObject Storage;
    public GameObject Settings;
    private GameObject _active;

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
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Reset()
    {
        Stats.Reset();
    }

    void Update()
    {
        if (!IsRunning) return;
        _percentToNextDay += DaySpeed * Time.deltaTime;
        if (_trySubtract(ref _percentToNextDay, 1f)) NextDay();
    }
    
    
    public void NextDay()
    {
        var foodChange = Action switch
        {
            Actions.Idle => -1,
            Actions.Fishing => 100,
            Actions.Woodcutting => -5,
            Actions.Mining => -5,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var stoneChange = Action switch
        {
            Actions.Idle => 0,
            Actions.Fishing => -5,
            Actions.Woodcutting => -5,
            Actions.Mining => 100,
            _ => throw new ArgumentOutOfRangeException()
        };

        var woodChange = Action switch
        {
            Actions.Idle => 0,
            Actions.Fishing => -5,
            Actions.Woodcutting => 100,
            Actions.Mining => -5,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (Stats.Food.Value + foodChange < 0) return;
        if (Stats.Stone.Value + stoneChange < 0) return;
        if (Stats.Wood.Value + woodChange < 0) return;

        Stats.Food.Value += foodChange;
        Stats.Stone.Value += stoneChange;
        Stats.Wood.Value += woodChange;
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
