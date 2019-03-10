using System;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class MeterToTilemapMixerAdapter : MonoBehaviour
    {
        [SerializeField]
        private DistanceEmissionMeter m_Meter;

        [SerializeField]
        private TilemapMixer m_Mixer;

        private Action<float> m_OnFillAmountUpdated;

        private void OnEnable()
        {
            if (m_OnFillAmountUpdated == null)
                m_OnFillAmountUpdated = SetMix;

            m_Meter.OnFillAmountUpdated -= m_OnFillAmountUpdated;
            m_Meter.OnFillAmountUpdated += m_OnFillAmountUpdated;
            m_Meter.UpdateView();
        }

        private void OnDisable()
        {
            m_Meter.OnFillAmountUpdated -= m_OnFillAmountUpdated;
        }

        private void SetMix(float mix)
        {
            m_Mixer.Mix = mix;
        }
    }
}
