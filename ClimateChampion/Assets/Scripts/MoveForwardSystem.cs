using System;
using System.Collections.Generic;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class MoveForwardSystem : ASingleton<MoveForwardSystem>
    {
        public static event Action<TravelerData> OnPositionChanged;

        private readonly List<TravelerData> m_Travelers = new List<TravelerData>();

        public MoveForwardSystem()
        {
            DeltaTimeSystem.OnDeltaTime -= Update;
            DeltaTimeSystem.OnDeltaTime += Update;
        }

        ~MoveForwardSystem()
        {
            DeltaTimeSystem.OnDeltaTime -= Update;
        }

        public void OnEnable(TravelerData traveler)
        {
            if (m_Travelers.Contains(traveler))
                return;

            m_Travelers.Add(traveler);
        }

        public void OnDisable(TravelerData traveler)
        {
            if (!m_Travelers.Contains(traveler))
                return;

            m_Travelers.Remove(traveler);
        }

        /// <summary>
        /// Reassigns struct to array. Otherwise would not be updated.
        /// </summary>
        public void Update(float deltaTime)
        {
            if (deltaTime == 0f)
                return;

            foreach (TravelerData traveler in m_Travelers)
            {
                if (traveler.speed == 0f)
                    continue;

                Vector2 direction = Vector2Utils.DegreeToVector2(traveler.rotation);
                traveler.position += direction * (traveler.speed * deltaTime);

                if (OnPositionChanged != null)
                    OnPositionChanged(traveler);
            }
        }
    }
}
