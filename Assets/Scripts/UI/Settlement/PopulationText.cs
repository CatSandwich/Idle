using TMPro;
using UnityEngine;

namespace UI.Settlement
{
    public class PopulationText : MonoBehaviour
    {
        public TextMeshProUGUI TMP;
    
        void Start() => SetText();
        void OnValidate() => SetText();

        public void SetText()
        {
            TMP.text = $"Population: {Stats.Housing}";
        }
    }
}
