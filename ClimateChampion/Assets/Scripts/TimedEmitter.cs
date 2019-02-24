using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class TimedEmitter : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_PrefabToSpawn;
        [SerializeField]
        private float m_RotationToSpawn;
        [SerializeField]
        private float m_RateOverTime;
        [SerializeField]
        private float m_RemainingTime;
        [SerializeField]
        private int m_NumEmissions;
        [SerializeField]
        private int m_NumEmissionsToDestroyEmitter;

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
            if (m_RateOverTime > m_RemainingTime)
                return;

            m_RemainingTime -= m_RateOverTime;
            m_NumEmissions++;

            if (m_PrefabToSpawn != null)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, m_RotationToSpawn);
                Instantiate(m_PrefabToSpawn, transform.position, rotation);
            }

            if (m_NumEmissionsToDestroyEmitter > 0 &&
                m_NumEmissions >= m_NumEmissionsToDestroyEmitter)
                Destroy(gameObject);
        }
    }
}
