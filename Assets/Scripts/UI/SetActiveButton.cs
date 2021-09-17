using UnityEngine;

namespace UI
{
    public class SetActiveButton : MonoBehaviour
    {
        public GameObject Obj;
        public ValueEnum Value;
    
        public void Click()
        {
            switch (Value)
            {
                case ValueEnum.On: 
                    Obj.SetActive(true);
                    break;
                case ValueEnum.Off: 
                    Obj.SetActive(false);
                    break;
                case ValueEnum.Toggle: 
                    Obj.SetActive(!Obj.activeSelf);
                    break;
            }
        }
    
        public enum ValueEnum
        {
            On,
            Off,
            Toggle
        }
    }
}
