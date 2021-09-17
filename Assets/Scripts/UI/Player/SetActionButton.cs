using UnityEngine;

namespace UI
{
    public class SetActionButton : MonoBehaviour
    {
        public GameManager.Actions Action;
        public void Click() => GameManager.Instance.Action = Action;
    }
}
