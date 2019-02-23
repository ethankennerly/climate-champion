using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class TravelerView : MonoBehaviour
    {
        [SerializeField]
        private TravelerData m_Data;

        private Action<Vector2> m_OnPositionChanged;

        private void OnEnable()
        {
            UpdateRotation(m_Data.rotation);

            UpdatePosition(m_Data.position);
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

        private void UpdatePosition(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }

        private void UpdateRotation(float degrees)
        {
            transform.eulerAngles = new Vector3(0f, 0f, degrees);
        }
    }
}
