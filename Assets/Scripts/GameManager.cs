using System;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        Home.SetActive(false);
        Player.SetActive(false);
        Settlement.SetActive(false);
        Storage.SetActive(false);
        Settings.SetActive(false);
        SetTab(Tabs.Home);
    }

    public void Reset()
    {
        Stats.Reset();
    } 

    public void SetTab(Tabs tab)
    {
        _active?.SetActive(false);
        _active = tab switch
        {
            Tabs.Home => Home,
            Tabs.Player => Player,
            Tabs.Settlement => Settlement,
            Tabs.Storage => Storage,
            Tabs.Settings => Settings,
            _ => throw new ArgumentOutOfRangeException(nameof(tab), tab, null)
        };
        _active.SetActive(true);
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
