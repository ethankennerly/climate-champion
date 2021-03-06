using System;
using System.Collections.Generic;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class MoveForwardSystem : ASingleton<MoveForwardSystem>
    {
        public static event Action<TravelerData> OnPositionChanged;
        public static event Action<TravelerData> OnArrived;

        private readonly List<TravelerData> m_Travelers = new List<TravelerData>();
        private readonly List<TravelerData> m_AddTravelers = new List<TravelerData>();
        private readonly List<TravelerData> m_RemoveTravelers = new List<TravelerData>();

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

            if (m_RemoveTravelers.Contains(traveler))
                m_RemoveTravelers.Remove(traveler);

            if (m_AddTravelers.Contains(traveler))
                return;

            m_AddTravelers.Add(traveler);
        }

        public void OnDisable(TravelerData traveler)
        {
            if (!m_Travelers.Contains(traveler))
                return;

            if (m_AddTravelers.Contains(traveler))
                m_AddTravelers.Remove(traveler);

            if (m_RemoveTravelers.Contains(traveler))
                return;

            m_RemoveTravelers.Remove(traveler);
        }

        public void Update(float deltaTime)
        {
            if (deltaTime == 0f)
                return;

            foreach (TravelerData traveler in m_AddTravelers)
                m_Travelers.Add(traveler);
            m_AddTravelers.Clear();
            foreach (TravelerData traveler in m_RemoveTravelers)
                m_Travelers.Remove(traveler);
            m_RemoveTravelers.Clear();

            foreach (TravelerData traveler in m_Travelers)
            {
                if (traveler.speed == 0f)
                    continue;

                traveler.deltaDistance = traveler.speed * deltaTime;
                Vector2 direction = Vector2Utils.DegreeToVector2(traveler.rotation);
                traveler.position += direction * traveler.deltaDistance;

                if (traveler.hasDestination)
                    UpdateDestination(traveler);

                if (OnPositionChanged != null)
                    OnPositionChanged(traveler);
            }
        }

        private static void UpdateDestination(TravelerData traveler)
        {
            Vector2 offset = traveler.destination - traveler.position;
            if (offset.magnitude > traveler.deltaDistance)
                return;

            traveler.position = traveler.destination;
            traveler.hasDestination = false;

            if (OnArrived != null)
                OnArrived(traveler);
        }
    }
}
