using UnityEngine;

namespace UI.Button_Toggle
{
    public class ToggleActive : MonoBehaviour
    {
        public GameObject[] Objects;

        public void Click()
        {
            foreach (var obj in Objects)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}