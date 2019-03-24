using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using TMPro;

namespace FineGameDesign.Utils
{
    public sealed class TimedEmitterStats : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text m_AverageRateOfEmissionsText;

        [SerializeField]
        private TMP_Text m_RatePerTopEmitterNameText;

        [Header("Calibrates for delayed start of emissions.")]
        [SerializeField]
        private float m_RateMultiplier = 3f;

        private class NamedCount : IComparer<NamedCount>
        {
            public int count;
            public string name;

            int IComparer<NamedCount>.Compare(NamedCount a, NamedCount b)
            {
                if (a.count > b.count)
                    return -1;

                if (a.count < b.count)
                    return 1;

                return string.Compare(a.name, b.name);
            }
        }

        private readonly NamedCount m_NamedCountComparer = new NamedCount();

        private readonly List<NamedCount> m_CountPerTopEmitterNames = new List<NamedCount>();

        [SerializeField]
        private int m_NumTopEmitters = 3;

        [SerializeField]
        private string m_CloneNamePrefix = "CarbonDioxideEmission";

        [SerializeField]
        private string m_EmitterCategoryPrefix = "";

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
            if (!StartsWith(clone == null ? null : clone.name, m_CloneNamePrefix))
                return;

            if (!StartsWith(emitter.StatsCategory, m_EmitterCategoryPrefix))
                return;

            m_NumEmissions++;
            string emitterName = TrimSuffix(emitter.name, "(Clone)");
            AddAmount(m_CountPerTopEmitterNames, emitterName, 1);

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

            duration /= m_RateMultiplier;

            m_AverageRateOfEmissions = (float)m_NumEmissions / duration;
            if (m_AverageRateOfEmissionsText != null)
            {
                int wholeRate = (int)Mathf.Round(m_AverageRateOfEmissions);
                m_AverageRateOfEmissionsText.text = wholeRate.ToString();
            }

            if (m_RatePerTopEmitterNameText != null)
            {
                m_CountPerTopEmitterNames.Sort(m_NamedCountComparer);

                m_RatePerTopEmitterNameText.text = FormatFirstRates(m_CountPerTopEmitterNames,
                    m_NumTopEmitters, duration);
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

        private static string TrimSuffix(string text, string suffix)
        {
            bool emptySuffix = string.IsNullOrEmpty(suffix);
            if (emptySuffix)
                return text;

            if (string.IsNullOrEmpty(text))
                return text;

            if (!text.EndsWith(suffix, StringComparison.Ordinal))
                return text;

            return text.Substring(0, text.Length - suffix.Length);
        }

        private static void AddAmount(List<NamedCount> namedCounts, string name, int amount)
        {
            foreach (NamedCount namedCount in namedCounts)
            {
                if (name != namedCount.name)
                    continue;
                namedCount.count += amount;
                return;
            }

            namedCounts.Add(new NamedCount()
            {
                count = amount,
                name = name
            });
        }

        private static string FormatFirstRates(List<NamedCount> countPerTopEmitterNames,
            int numTopEmitters, float duration)
        {
            var builder = new StringBuilder();
            int numEmitters = countPerTopEmitterNames.Count;
            if (numEmitters > numTopEmitters)
                numEmitters = numTopEmitters;

            for (int emitterIndex = 0; emitterIndex < numEmitters; ++emitterIndex)
            {
                NamedCount namedCount = countPerTopEmitterNames[emitterIndex];
                builder.Append(namedCount.name);
                builder.Append(": ");
                float rate = (float)namedCount.count / duration;
                string rateDecimalText = rate.ToString("N1");
                builder.Append(rateDecimalText);
                builder.Append('\n');
            }

            return builder.ToString();
        }
    }
}
