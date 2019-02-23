using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class TimerSystemView : ASingletonView<TimerSystem>
    {
        private void Update()
        {
            Controller.Update(Time.deltaTime);
        }
    }
}
