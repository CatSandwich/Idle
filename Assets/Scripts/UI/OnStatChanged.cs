using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class OnStatChanged : MonoBehaviour
    {
        public UnityEvent Callbacks;

        void Start() => Stats.StatChanged += Callbacks.Invoke;
    }
}
