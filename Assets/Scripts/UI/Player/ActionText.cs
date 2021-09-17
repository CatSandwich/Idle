using System;
using TMPro;
using UnityEngine;

namespace UI.Player
{
    public class ActionText : MonoBehaviour
    {

        private TextMeshProUGUI _text;
        public void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            
            void setText(GameManager.Actions action)
            {
                _text.text = @$"Action: {action switch
                {
                    GameManager.Actions.Fishing => "Fishing",
                    GameManager.Actions.Idle => "Idle",
                    GameManager.Actions.Woodcutting => "Woodcutting",
                    GameManager.Actions.Mining => "Mining",
                    _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)}}";
            }
            
            setText(GameManager.Instance.Action);
            GameManager.Instance.ActionChanged += setText;
        }
    }
}
