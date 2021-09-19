using UnityEngine;

namespace UI
{
    public class PurchaseButton : MonoBehaviour
    {
        public ResourceDelta Outcome;

        public void Click()
        {
            if ((Outcome + Stats.ToDelta()).HasNegative()) return;
            Stats.ApplyDelta(Outcome);
        }
    }
}
