using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHouse : MonoBehaviour
{
    // Start is called before the first frame update
    public void Build()
    {
        if (Stats.Wood.Value < Stats.BasicHouseCost.wood || Stats.Stone.Value < Stats.BasicHouseCost.stone) return;
        
        Stats.Wood.Value -= Stats.BasicHouseCost.wood;
        Stats.Stone.Value -= Stats.BasicHouseCost.stone;
        Stats.BasicHouses.Value++;
    }
}
