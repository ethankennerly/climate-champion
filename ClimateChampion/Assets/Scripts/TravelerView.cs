using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class TravelerView : MonoBehaviour
    {
        public event Action<TravelerData> OnPositionChanged;

        [SerializeField]
        private TravelerData m_Data;
        public TravelerData Data
        {
            get { return m_Data; }
        }

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

        public static bool TrySetDestination(GameObject travelerObject, Transform destination)
        {
            if (destination == null)
                return false;

            SetDestination(travelerObject, destination.position);
            return true;
        }

        /// <returns>
        /// Estimated remaining time until arrival.
        /// </returns>
        public static float SetDestination(GameObject travelerObject, Vector2 destination)
        {
            TravelerView view = travelerObject.GetComponent<TravelerView>();
            if (view == null)
                return 0f;

            TravelerData traveler = view.Data;
            Vector2 offset = destination - traveler.position;
            float distance = offset.magnitude;
            if (distance == 0f)
                return 0f;

            if (traveler.speed <= 0f)
                return 0f;

            traveler.rotation = Vector2Utils.AngleBetweenPoints(traveler.position, destination);
            float duration = distance / traveler.speed;
            return duration;
        }
    }
}
