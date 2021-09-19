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
            Stats.StatChanged += _updateText;
            GameManager.Instance.ActionChanged += _updateText;
            _updateText();
        }

        private void OnValidate() => _updateText();

        private void _updateText()
        {
            if (!TMP)
            {
                #if UNITY_EDITOR
                Debug.LogError("TMP not set on GenericLabel.");
                #endif
                return;
            }

            try { TMP.text = Stats.ReplaceVariables(Text); }
            catch (Exception e) { Debug.LogException(e, gameObject); }
        }
    }
}
