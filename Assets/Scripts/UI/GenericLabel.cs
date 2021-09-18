using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GenericLabel : MonoBehaviour
    {
        public TextMeshProUGUI TMP;
        [TextArea]
        public string Text;

        public void Start()
        {
            Stats.StatChanged += stats => _updateText();
            GameManager.Instance.ActionChanged += action => _updateText();
            _updateText();
        }

        private void OnValidate() => _updateText();

        void _updateText()
        {
            if (!TMP)
            {
                #if UNITY_EDITOR
                Debug.LogError("TMP not set on GenericLabel.");
                #endif
                return;
            }

            try
            {
                TMP.text = Text
                    .Replace("{{ Food }}", Stats.Food.Value.ToString())
                    .Replace("{{ Wood }}", Stats.Wood.Value.ToString())
                    .Replace("{{ Stone }}", Stats.Stone.Value.ToString())
                    .Replace("{{ BasicHouses }}", Stats.BasicHouses.Value.ToString())
                    .Replace("{{ Population }}", Stats.Population.Value.ToString())
                    .Replace("{{ AvailablePopulation }}", Stats.AvailablePopulation.ToString())
                    .Replace("{{ BasicHouseCostWood }}", Stats.BasicHouseCost.wood.ToString())
                    .Replace("{{ BasicHouseCostStone }}", Stats.BasicHouseCost.stone.ToString())
                    .Replace("{{ Action }}", GameManager.Instance.Action.ToString());
            }
            catch (Exception e)
            {
                Debug.LogException(e, gameObject);
            }
        }
    }
}
