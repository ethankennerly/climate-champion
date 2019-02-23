using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class DeltaTimeSystemView : ASingletonView<DeltaTimeSystem>
    {
        public void Update()
        {
            Controller.Update(Time.deltaTime);
        }
    }
}
