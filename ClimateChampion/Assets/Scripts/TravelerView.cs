using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class TravelerView : MonoBehaviour
    {
        public event Action<TravelerData> OnPositionChanged;

        [SerializeField]
        private TravelerData m_Data;

        private Action<TravelerData> m_OnPositionChanged;

        private void OnEnable()
        {
            m_Data.position = transform.position;

            UpdateRotation(m_Data.rotation);

            UpdatePosition(m_Data);
            if (m_OnPositionChanged == null)
                m_OnPositionChanged = UpdatePosition;

            MoveForwardSystem.OnPositionChanged -= m_OnPositionChanged;
            MoveForwardSystem.OnPositionChanged += m_OnPositionChanged;
            MoveForwardSystem.Instance.OnEnable(m_Data);
        }

        private void OnDisable()
        {
            if (MoveForwardSystem.InstanceExists())
                MoveForwardSystem.Instance.OnDisable(m_Data);
            MoveForwardSystem.OnPositionChanged -= m_OnPositionChanged;
        }

        private void UpdatePosition(TravelerData traveler)
        {
            if (traveler != m_Data)
                return;

            transform.position = new Vector3(traveler.position.x, traveler.position.y, transform.position.z);
            if (OnPositionChanged != null)
                OnPositionChanged(traveler);
        }

        private void UpdateRotation(float degrees)
        {
            transform.eulerAngles = new Vector3(0f, 0f, degrees);
        }
    }
}
