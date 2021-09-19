using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Stats
{
    
    public static readonly Dictionary<Resource, Stat> Resources = new Dictionary<Resource, Stat>();
    public static readonly Dictionary<string, Func<string>> Variables = new Dictionary<string, Func<string>>();
    public static readonly Dictionary<GameManager.Actions, ResourceDelta> ActionResourceDeltas = new Dictionary<GameManager.Actions, ResourceDelta>();
    
    public static event Action StatChanged;
    
    public static int Housing => Resources[Resource.BasicHouses].Value;
    public static int AvailablePopulation => Resources[Resource.Population].Value;
    
    public static ResourceDelta BasicHouseCost = new ResourceDelta {Stone = 100, Wood = 100};

    static Stats()
    {
        Resources[Resource.Food] = new Stat("Food", "food", 1000);
        Resources[Resource.Wood] = new Stat("Wood","wood", 1000);
        Resources[Resource.Stone] = new Stat("Stone", "stone", 1000);
        Resources[Resource.Gold] = new Stat("Gold", "gold", 1000);
        Resources[Resource.Diamonds] = new Stat("Diamonds", "diamonds", 100);
        Resources[Resource.Population] = new Stat("Population", "population", 0);
        Resources[Resource.BasicHouses] = new Stat("Basic Houses", "basic_houses", 0);

        Variables["Food"] = () => Resources[Resource.Food].Value.ToString();
        Variables["Wood"] = () => Resources[Resource.Wood].Value.ToString();
        Variables["Stone"] = () => Resources[Resource.Stone].Value.ToString();
        Variables["Gold"] = () => Resources[Resource.Gold].Value.ToString();
        Variables["Diamonds"] = () => Resources[Resource.Diamonds].Value.ToString();
        Variables["BasicHouses"] = () => Resources[Resource.BasicHouses].Value.ToString();
        Variables["Housing"] = () => Housing.ToString();
        Variables["Population"] = () => Resources[Resource.Population].Value.ToString();
        Variables["AvailablePopulation"] = () => AvailablePopulation.ToString();
        Variables["BasicHouseCostWood"] = () => BasicHouseCost.Wood.ToString();
        Variables["BasicHouseCostStone"] = () => BasicHouseCost.Stone.ToString();
        Variables["Action"] = () => GameManager.Instance.Action.ToString();

        ActionResourceDeltas[GameManager.Actions.Woodcutting] = new ResourceDelta
        {
            Wood = 100,
            Food = -5,
            Stone = -5
        };
        ActionResourceDeltas[GameManager.Actions.Idle] = new ResourceDelta
        {
            Food = -1
        };
        ActionResourceDeltas[GameManager.Actions.Mining] = new ResourceDelta
        {
            Stone = 100,
            Food = -5,
            Wood = -5
        };
        ActionResourceDeltas[GameManager.Actions.Fishing] = new ResourceDelta
        {
            Food = 100,
            Stone = -5,
            Wood = -5
        };
    }

    public static ResourceDelta ToDelta() => new ResourceDelta
    {
        Wood = Resources[Resource.Wood].Value,
        Food = Resources[Resource.Food].Value,
        Stone = Resources[Resource.Stone].Value,
        Gold = Resources[Resource.Gold].Value,
        Diamonds = Resources[Resource.Diamonds].Value,
        BasicHouses = Resources[Resource.BasicHouses].Value,
        Population = Resources[Resource.Population].Value
    };

    public static void ApplyDelta(ResourceDelta delta)
    {
        Resources[Resource.Wood].Value += delta.Wood;
        Resources[Resource.Food].Value += delta.Food;
        Resources[Resource.Stone].Value += delta.Stone;
        Resources[Resource.Gold].Value += delta.Gold;
        Resources[Resource.Diamonds].Value += delta.Diamonds;
        Resources[Resource.BasicHouses].Value += delta.BasicHouses;
        Resources[Resource.Population].Value += delta.Population;
    }
    
    public static string ReplaceVariables(string str)
    {
        var sb = new StringBuilder(str);
        foreach (var variable in Variables)
        {
            sb.Replace($@"{{{{ {variable.Key} }}}}", variable.Value());
        }
        return sb.ToString();
    }
    
    public static void Reset()
    {
        foreach (var stat in Resources.Values)
        {
            stat.Reset();
        }
    }

    public enum Resource
    {
        Food,
        Wood,
        Stone,
        Gold,
        Diamonds,
        Population,
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
                StatChanged?.Invoke();
            }
        }

        public event Action<int> ValueChanged = null;

        public readonly string Name;
        private readonly string _key;
        private readonly int _default;

        public Stat(string name, string key, int @default)
        {
            Name = name;
            _key = key;
            _default = @default;
        }

        public void Reset() => Value = _default;
        public override string ToString() => ToString(true);
        public string ToString(bool label) => label ? $"{Name}: " : "" + Value;
    }
}

[Serializable]
public struct ResourceDelta
{
    public int Wood;
    public int Stone;
    public int Food;
    public int Gold;
    public int Diamonds;
    public int Population;
    public int BasicHouses;

    public bool HasNegative()
    {
        return Wood < 0 || 
               Stone < 0 || 
               Food < 0 || 
               Gold < 0 || 
               Diamonds < 0 || 
               Population < 0 || 
               BasicHouses < 0;
    }

    public static ResourceDelta operator +(ResourceDelta lhs, ResourceDelta rhs)
    {
        return new ResourceDelta
        {
            Wood = lhs.Wood + rhs.Wood,
            Stone = lhs.Stone + rhs.Stone,
            Food = lhs.Food + rhs.Food,
            Gold = lhs.Gold + rhs.Gold,
            Diamonds = lhs.Diamonds + rhs.Diamonds,
            Population = lhs.Population + rhs.Population,
            BasicHouses = lhs.BasicHouses + rhs.BasicHouses,
        };
    }
}