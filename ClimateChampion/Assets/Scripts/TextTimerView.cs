using System;
using TMPro;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class TextTimerView : MonoBehaviour
    {
        [SerializeField]
        private TimerData m_Data;

        [SerializeField]
        private TMP_Text m_Text;

        private Action<TimerData> m_OnWholeChanged;

        private void OnEnable()
        {
            UpdateText(m_Data);
            if (m_OnWholeChanged == null)
                m_OnWholeChanged = UpdateText;

            TimerSystem.OnWholeChanged -= m_OnWholeChanged;
            TimerSystem.OnWholeChanged += m_OnWholeChanged;
            TimerSystem.Instance.OnEnable(m_Data);
        }

        private void OnDisable()
        {
            if (TimerSystem.InstanceExists())
                TimerSystem.Instance.OnDisable(m_Data);
            TimerSystem.OnWholeChanged -= m_OnWholeChanged;
        }

        private void UpdateText(TimerData timer)
        {
            if (timer != m_Data)
                return;

            m_Text.text = timer.whole.ToString();
        }
    }
}
