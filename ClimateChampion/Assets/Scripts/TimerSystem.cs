using System;
using System.Collections.Generic;

namespace FineGameDesign.Utils
{
    public sealed class TimerSystem : ASingleton<TimerSystem>
    {
        public static event Action<TimerData> OnWholeChanged;

        private readonly List<TimerData> m_Timers = new List<TimerData>();

        public TimerSystem()
        {
            DeltaTimeSystem.OnDeltaTime -= Update;
            DeltaTimeSystem.OnDeltaTime += Update;
        }

        ~TimerSystem()
        {
            DeltaTimeSystem.OnDeltaTime -= Update;
        }

        public void OnEnable(TimerData timer)
        {
            if (m_Timers.Contains(timer))
                return;

            m_Timers.Add(timer);
        }

        public void OnDisable(TimerData timer)
        {
            if (!m_Timers.Contains(timer))
                return;

            m_Timers.Remove(timer);
        }

        /// <summary>
        /// Reassigns struct to array. Otherwise would not be updated.
        /// </summary>
        public void Update(float deltaTime)
        {
            foreach (TimerData timer in m_Timers)
            {
                timer.remainder += timer.speed * deltaTime;
                if (timer.remainder < 1f)
                    continue;

                int adding = (int)timer.remainder;
                timer.whole += adding;
                timer.remainder -= adding;
                if (OnWholeChanged != null)
                    OnWholeChanged(timer);
            }
        }
    }
}
