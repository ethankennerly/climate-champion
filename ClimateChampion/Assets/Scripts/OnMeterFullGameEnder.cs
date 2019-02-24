using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class OnMeterFullGameEnder : MonoBehaviour
    {
        [SerializeField]
        private ItemType m_EmissionType;

        [SerializeField]
        private GameObject m_EndGameRoot;

        [SerializeField]
        private GameObject m_ActiveDuringPlay;

        [SerializeField]
        private DeltaTimeSystemView m_DeltaTime;

        private Action<ItemType> m_OnFull;

        private void OnEnable()
        {
            m_OnFull = EndGame;
            DistanceEmissionMeter.OnFull -= m_OnFull;
            DistanceEmissionMeter.OnFull += m_OnFull;
        }

        private void OnDisable()
        {
            DistanceEmissionMeter.OnFull -= m_OnFull;
        }

        private void EndGame(ItemType emissionType)
        {
            if (m_EmissionType != emissionType)
                return;

            m_DeltaTime.enabled = false;
            m_EndGameRoot.SetActive(true);
            m_ActiveDuringPlay.SetActive(false);
        }
    }
}
