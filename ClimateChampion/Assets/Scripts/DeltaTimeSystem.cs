using System;

namespace FineGameDesign.Utils
{
    public sealed class DeltaTimeSystem : ASingleton<DeltaTimeSystem>
    {
        public static event Action<float> OnDeltaTime;

        private bool m_Paused;
        public bool Paused
        {
            get { return m_Paused; }
            set { m_Paused = value; }
        }

        public void Update(float deltaTime)
        {
            deltaTime = m_Paused ? 0f : deltaTime;
            if (OnDeltaTime == null || deltaTime == 0f)
            {
                return;
            }
            OnDeltaTime(deltaTime);
        }
    }
}
