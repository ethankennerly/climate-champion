using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class OnMeterFullGameEnder : MonoBehaviour
    {
        [SerializeField]
        private ItemType m_EmissionType;

        [SerializeField]
        private GameObject m_WinGameRoot;

        [SerializeField]
        private int m_WinGameTime;

        [SerializeField]
        private GameObject m_EndGameRoot;

        [SerializeField]
        private GameObject m_ActiveDuringPlay;

        [SerializeField]
        private DeltaTimeSystemView m_DeltaTime;

        private Action<ItemType> m_OnFull;
        private Action<TimerData> m_OnWholeChanged;

        private void OnEnable()
        {
            if (m_OnFull == null)
                m_OnFull = EndGame;
            DistanceEmissionMeter.OnFull -= m_OnFull;
            DistanceEmissionMeter.OnFull += m_OnFull;

            if (m_OnWholeChanged == null)
                m_OnWholeChanged = WinGameIfTimeUp;
            TimerSystem.OnWholeChanged -= m_OnWholeChanged;
            TimerSystem.OnWholeChanged += m_OnWholeChanged;
        }

        private void OnDisable()
        {
            DistanceEmissionMeter.OnFull -= m_OnFull;
            TimerSystem.OnWholeChanged -= m_OnWholeChanged;
        }

        private void EndGame(ItemType emissionType)
        {
            if (m_EmissionType != emissionType)
                return;

            m_DeltaTime.enabled = false;
            m_ActiveDuringPlay.SetActive(false);
            m_EndGameRoot.SetActive(true);
        }

        private void WinGameIfTimeUp(TimerData timer)
        {
            if (timer.whole < m_WinGameTime)
                return;

            m_DeltaTime.enabled = false;
            m_ActiveDuringPlay.SetActive(false);
            m_WinGameRoot.SetActive(true);
        }
    }
}
