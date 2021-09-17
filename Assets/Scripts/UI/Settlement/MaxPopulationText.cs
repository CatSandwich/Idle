using TMPro;
using UnityEngine;

namespace UI.Settlement
{
    public class MaxPopulationText : MonoBehaviour
    {
        public TextMeshProUGUI TMP;
    
        void Start() => SetText();
        void OnValidate() => SetText();

        void SetText()
        {
            TMP.text = $"Max Population: {Stats.MaxPopulation}";
        }
    }
}
