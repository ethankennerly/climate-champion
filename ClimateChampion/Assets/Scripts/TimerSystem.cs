using System;
using System.Collections.Generic;

namespace FineGameDesign.Utils
{
    public sealed class TimerSystem : ASingleton<TimerSystem>
    {
        public static event Action<int> OnWholeChanged;

        private readonly List<TimerData> m_Timers = new List<TimerData>();

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
            for (int index = 0, numTimers = m_Timers.Count; index < numTimers; ++index)
            {
                TimerData timer = m_Timers[index];
                timer.remainder += timer.speed * deltaTime;
                if (timer.remainder < 1f)
                {
                    m_Timers[index] = timer;
                    continue;
                }

                int adding = (int)timer.remainder;
                timer.whole += adding;
                timer.remainder -= adding;
                m_Timers[index] = timer;
                if (OnWholeChanged != null)
                    OnWholeChanged(timer.whole);
            }
        }
    }
}
