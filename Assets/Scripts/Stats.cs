using System;
using UnityEngine;

public static class Stats
{
    public static Stat[] StatArray => new[]{Food, Stone, Wood, BasicHouses, Population};
    public static StatValues Values => new StatValues(Food.Value, Wood.Value, Stone.Value, BasicHouses.Value, Population.Value);

    
    public static event Action<StatValues> StatChanged;

    public static Stat GetResource(Resource resource) => resource switch
    {
        Resource.Food => Food,
        Resource.Wood => Wood,
        Resource.Stone => Stone,
        Resource.BasicHouses => BasicHouses,
        _ => throw new ArgumentOutOfRangeException(nameof(resource), resource, null)
    };
    
    #region Food
    private static int ActionFoodGain => GameManager.Instance.Action switch
    {
        GameManager.Actions.Idle => -1,
        GameManager.Actions.Fishing => 100,
        GameManager.Actions.Woodcutting => -5,
        GameManager.Actions.Mining => -5,
        _ => throw new ArgumentOutOfRangeException($"Invalid action {GameManager.Instance.Action}.")
    };

    //private static int FoodGainPerPopulation => GameManager.Instance.Population * -1;
    
    public static readonly Stat Food = new Stat("Food", "food", 1000, () => ActionFoodGain /*+ FoodGainPerPopulation*/);
    #endregion
    
    #region Stone
    private static int ActionStoneGain => GameManager.Instance.Action switch
    {
        GameManager.Actions.Idle => 0,
        GameManager.Actions.Fishing => -5,
        GameManager.Actions.Woodcutting => -5,
        GameManager.Actions.Mining => 100,
        _ => throw new ArgumentOutOfRangeException()
    };
    public static readonly Stat Stone = new Stat("Stone", "stone", 1000, () => ActionStoneGain);
    #endregion
    
    #region Wood
    private static int ActionWoodGain => GameManager.Instance.Action switch
    {
        GameManager.Actions.Idle => 0,
        GameManager.Actions.Fishing => -5,
        GameManager.Actions.Woodcutting => 100,
        GameManager.Actions.Mining => -5,
        _ => throw new ArgumentOutOfRangeException()
    };
    public static readonly Stat Wood = new Stat("Wood","wood", 1000, () => ActionWoodGain);
    #endregion

    #region Population
    public static int Housing => BasicHouses.Value;
    public static readonly Stat Population = new Stat("Population", "population", 0);
    public static int AvailablePopulation => Population.Value;
    
    #endregion

    #region Houses
    public static (int wood, int stone) BasicHouseCost => (100, 100);
    public static readonly Stat BasicHouses = new Stat("Basic Houses", "basic_houses", 0);
    #endregion
    
    public static void Reset()
    {
        foreach (var stat in StatArray)
        {
            stat.Reset();
        }
    }
    
    public enum Resource
    {
        Food,
        Wood,
        Stone,
        BasicHouses
    }
    
    public class Stat
    {
        public int Value
        {
            get => PlayerPrefs.GetInt(_key, _default);
            set
            {
                PlayerPrefs.SetInt(_key, value);
                ValueChanged?.Invoke(value);
                StatChanged?.Invoke(Values);
            }
        }

        public bool IsValid => Value + _calcDailyChange() >= 0;

        public event Action<int> ValueChanged = null;
        private readonly Func<int> _calcDailyChange;

        public readonly string Name;
        private readonly string _key;
        private readonly int _default;

        public Stat(string name, string key, int @default, Func<int> calcDailyChange = null)
        {
            Name = name;
            _key = key;
            _default = @default;
            _calcDailyChange = calcDailyChange ?? (() => 0);
        }

        public void NextDay() => Value += _calcDailyChange();
        public void Reset() => Value = _default;
        public override string ToString() => ToString(true);
        public string ToString(bool label) => label ? $"{Name}: " : "" + Value;
    }
}

public readonly struct StatValues
{
    public readonly int Food;
    public readonly int Wood;
    public readonly int Stone;
    public readonly int BasicHouses;
    public readonly int MaxPopulation;

    public StatValues(int food, int wood, int stone, int basicHouses, int maxPopulation)
    {
        Food = food;
        Wood = wood;
        Stone = stone;
        BasicHouses = basicHouses;
        MaxPopulation = maxPopulation;
    }
}