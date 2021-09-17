using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenericLabel : MonoBehaviour
{
    public TextMeshProUGUI TMP;
    [TextArea]
    public string Text;

    public void Start()
    {
        Stats.StatChanged += stats => _updateText();
        _updateText();
    }

    private void OnValidate()
    {
        _updateText();
    }

    void _updateText()
    {
        if (!TMP) return;
        
        TMP.text = Text
            .Replace("{{ Food }}", Stats.Food.Value.ToString())
            .Replace("{{ Wood }}", Stats.Wood.Value.ToString())
            .Replace("{{ Stone }}", Stats.Stone.Value.ToString())
            .Replace("{{ BasicHouses }}", Stats.BasicHouses.Value.ToString())
            .Replace("{{ BasicHouseCostWood }}", Stats.BasicHouseCost.wood.ToString())
            .Replace("{{ BasicHouseCostStone }}", Stats.BasicHouseCost.stone.ToString());
    }
}
