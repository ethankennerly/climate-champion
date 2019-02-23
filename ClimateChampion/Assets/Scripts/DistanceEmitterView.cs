using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    [RequireComponent(typeof(TravelerView))]
    public sealed class DistanceEmitterView : MonoBehaviour
    {
        public static event Action<DistanceEmitterData, TravelerData> OnDistanceChanged;

        [SerializeField]
        private DistanceEmitterData m_Data;

        private Action<TravelerData> m_OnPositionChanged;

        private TravelerView m_TravelerPublisher;

        private void OnEnable()
        {
            if (m_OnPositionChanged == null)
                m_OnPositionChanged = UpdateDistance;

            if (m_TravelerPublisher == null)
                m_TravelerPublisher = gameObject.GetComponent<TravelerView>();

            m_TravelerPublisher.OnPositionChanged -= m_OnPositionChanged;
            m_TravelerPublisher.OnPositionChanged += m_OnPositionChanged;
        }

        private void OnDisable()
        {
            m_TravelerPublisher.OnPositionChanged -= m_OnPositionChanged;
        }

        private void UpdateDistance(TravelerData traveler)
        {
            if (OnDistanceChanged != null)
                OnDistanceChanged(m_Data, traveler);
        }
    }
}
