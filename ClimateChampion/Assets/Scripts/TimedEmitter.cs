using System;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class TimedEmitter : MonoBehaviour
    {
        public static event Action<TimedEmitter, GameObject> OnEmitted;
        public static event Action<TimedEmitter> OnDestroyed;
        public event Action<float> OnProgressChanged;

        [SerializeField]
        private GameObject m_PrefabToSpawn;
        public GameObject PrefabToSpawn
        {
            get { return m_PrefabToSpawn; }
            set { m_PrefabToSpawn = value; }
        }

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
        private float m_AccumulatedTime;
        public float AccumulatedTime
        {
            get { return m_AccumulatedTime; }
            set { m_AccumulatedTime = value; }
        }

        private float m_Progress = -1f;
        public float Progress
        {
            get { return m_Progress; }
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
            m_AccumulatedTime += deltaTime;
            UpdateProgress();
            if (m_RateOverTime > m_AccumulatedTime)
                return;

            m_AccumulatedTime -= m_RateOverTime;
            m_NumEmissions++;

            GameObject clone = null;

            if (m_PrefabToSpawn != null)
            {
                Quaternion spawnRotation = Quaternion.Euler(0f, 0f, m_RotationToSpawn);
                Vector3 spawnPosition = m_OffsetWorld ? Vector3.zero : transform.position;
                spawnPosition += m_OffsetPerSpawn * m_NumEmissions;
                clone = Instantiate(m_PrefabToSpawn, spawnPosition, spawnRotation);
            }

            if (OnEmitted != null)
                OnEmitted(this, clone);

            if (m_NumEmissionsToDestroyEmitter > 0 &&
                m_NumEmissions >= m_NumEmissionsToDestroyEmitter)
            {
                if (OnDestroyed != null)
                    OnDestroyed(this);
                Destroy(gameObject);
            }
        }

        private void UpdateProgress()
        {
            float previousProgress = m_Progress;
            m_Progress = m_RateOverTime == 0f ?
                1f :
                m_AccumulatedTime / m_RateOverTime;
            m_Progress = Mathf.Clamp01(m_Progress);
            if (previousProgress == m_Progress)
                return;

            UpdateFill();

            if (OnProgressChanged == null)
                return;
            OnProgressChanged(m_Progress);
        }

        private void UpdateFill()
        {
            if (m_FillImage == null)
                return;

            m_FillImage.fillAmount = m_Progress;
        }
    }
}
