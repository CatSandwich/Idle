using UnityEngine;

namespace UI
{
    public class SetActive : MonoBehaviour
    {
        public GameObject[] Enable;
        public GameObject[] Disable;
        public GameObject[] Toggle;

        public void Click()
        {
            foreach (var obj in Enable)
            {
                obj.SetActive(true);
            }
            foreach (var obj in Disable)
            {
                obj.SetActive(false);
            }
            foreach (var obj in Toggle)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}
