using UnityEngine;

namespace UI
{
    public class ResetButton : MonoBehaviour
    {
        public void Click() => GameManager.Instance.Reset();
    }
}
