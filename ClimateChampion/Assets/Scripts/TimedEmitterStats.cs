using System;
using UnityEngine;
using TMPro;

namespace FineGameDesign.Utils
{
    public sealed class TimedEmitterStats : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text m_AverageRateOfEmissionsText;

        [SerializeField]
        private string m_CloneNamePrefix = "CarbonDioxideEmission";
        [SerializeField]
        private string m_EmitterNamePrefix = "";

        [SerializeField]
        private float m_MinDuration = 1.0f;

        private int m_NumEmissions;
        private float m_Duration;
        private float m_AverageRateOfEmissions;

        private Action<TimedEmitter, GameObject> m_OnEmitted;

        private Action<float> m_OnDeltaTime;

        private void AddDeltaTimeListener()
        {
            if (m_OnDeltaTime == null)
                m_OnDeltaTime = AddDuration;

            DeltaTimeSystem.OnDeltaTime -= m_OnDeltaTime;
            DeltaTimeSystem.OnDeltaTime += m_OnDeltaTime;
        }

        private void RemoveDeltaTimeListener()
        {
            DeltaTimeSystem.OnDeltaTime -= m_OnDeltaTime;
        }

        private void OnEnable()
        {
            if (m_OnEmitted == null)
                m_OnEmitted = IncrementNumEmissions;

            TimedEmitter.OnEmitted -= m_OnEmitted;
            TimedEmitter.OnEmitted += m_OnEmitted;

            AddDeltaTimeListener();
        }

        private void OnDisable()
        {
            TimedEmitter.OnEmitted -= m_OnEmitted;

            RemoveDeltaTimeListener();
        }

        private void IncrementNumEmissions(TimedEmitter emitter, GameObject clone)
        {
            if (!StartsWith(clone.name, m_CloneNamePrefix))
                return;

            if (!StartsWith(emitter.name, m_EmitterNamePrefix))
                return;

            m_NumEmissions++;

            UpdateAverageRateOfEmissions();
        }
        
        private void AddDuration(float deltaTime)
        {
            m_Duration += deltaTime;

            UpdateAverageRateOfEmissions();
        }

        private void UpdateAverageRateOfEmissions()
        {
            float duration = m_Duration < m_MinDuration ?
                m_MinDuration :
                m_Duration;

            m_AverageRateOfEmissions = (float)m_NumEmissions / duration;
            if (m_AverageRateOfEmissionsText != null)
            {
                int wholeRate = (int)Mathf.Round(m_AverageRateOfEmissions);
                m_AverageRateOfEmissionsText.text = wholeRate.ToString();
            }
        }

        private static bool StartsWith(string text, string prefix)
        {
            bool emptyPrefix = string.IsNullOrEmpty(prefix);
            if (emptyPrefix)
                return true;

            if (string.IsNullOrEmpty(text))
                return false;

            return text.StartsWith(prefix, StringComparison.Ordinal);
        }
    }
}
