using System;
using TMPro;
using UnityEngine;

namespace UI.Nav
{
    [ExecuteAlways]
    public class StatText : MonoBehaviour
    {
        public bool ShowLabel;
        public TextMeshProUGUI Text;
        public Stats.Resource Resource;

        void Start()
        {
            SetText();
            Stats.GetResource(Resource).ValueChanged += val => SetText();
        }

        private void OnValidate() => SetText();
        private void SetText() => Text.text = Stats.GetResource(Resource).ToString(ShowLabel);
    }
}
