using UnityEngine;

namespace UI
{
    public class TabButton : MonoBehaviour
    {
        public GameManager.Tabs Tab;
        public void Click()
        {
            GameManager.Instance.SetTab(Tab);
        }
    }
}
