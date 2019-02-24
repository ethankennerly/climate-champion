using System;

namespace FineGameDesign.Utils
{
    public sealed class DeltaTimeSystem : ASingleton<DeltaTimeSystem>
    {
        public static event Action<float> OnDeltaTime;

        public void Update(float deltaTime)
        {
            if (OnDeltaTime != null)
                OnDeltaTime(deltaTime);
        }
    }
}
