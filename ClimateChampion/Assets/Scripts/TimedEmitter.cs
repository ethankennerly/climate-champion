using System;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class TimedEmitter : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_PrefabToSpawn;
        [SerializeField]
        private float m_RotationToSpawn;
        [SerializeField]
        private Vector3 m_OffsetPerSpawn;
        [SerializeField]
        private bool m_OffsetWorld;
        [SerializeField]
        private float m_RateOverTime;
        public float RateOverTime
        {
            get { return m_RateOverTime; }
            set { m_RateOverTime = value; }
        }

        [SerializeField]
        private float m_RemainingTime;
        public float RemainingTime
        {
            get { return m_RemainingTime; }
            set { m_RemainingTime = value; }
        }

        [SerializeField]
        private int m_NumEmissions;
        [SerializeField]
        private int m_NumEmissionsToDestroyEmitter;

        [SerializeField]
        private Image m_FillImage;

        private Action<float> m_OnDeltaTime;

        private void OnEnable()
        {
            if (m_OnDeltaTime == null)
                m_OnDeltaTime = UpdateEmission;

            DeltaTimeSystem.OnDeltaTime -= m_OnDeltaTime;
            DeltaTimeSystem.OnDeltaTime += m_OnDeltaTime;
        }

        private void OnDisable()
        {
            DeltaTimeSystem.OnDeltaTime -= m_OnDeltaTime;
        }

        private void UpdateEmission(float deltaTime)
        {
            m_RemainingTime += deltaTime;
            UpdateFill();
            if (m_RateOverTime > m_RemainingTime)
                return;

            m_RemainingTime -= m_RateOverTime;
            m_NumEmissions++;

            if (m_PrefabToSpawn != null)
            {
                Quaternion spawnRotation = Quaternion.Euler(0f, 0f, m_RotationToSpawn);
                Vector3 spawnPosition = m_OffsetWorld ? Vector3.zero : transform.position;
                spawnPosition += m_OffsetPerSpawn * m_NumEmissions;
                Instantiate(m_PrefabToSpawn, spawnPosition, spawnRotation);
            }

            if (m_NumEmissionsToDestroyEmitter > 0 &&
                m_NumEmissions >= m_NumEmissionsToDestroyEmitter)
                Destroy(gameObject);
        }

        private void UpdateFill()
        {
            if (m_FillImage == null)
                return;

            float progress = m_RateOverTime == 0f ?
                1f :
                (m_RateOverTime - m_RemainingTime) / m_RateOverTime;
            progress = Mathf.Clamp01(progress);
            m_FillImage.fillAmount = progress;
        }
    }
}
