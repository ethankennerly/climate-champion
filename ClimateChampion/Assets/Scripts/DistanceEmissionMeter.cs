using System;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class DistanceEmissionMeter : MonoBehaviour
    {
        private Action<TravelerData, ItemType> m_Emit;

        [SerializeField]
        private ItemType m_EmissionType;

        [SerializeField]
        private float m_Capacity;

        [SerializeField]
        private float m_Quantity;

        [SerializeField]
        private float m_QuantityPerEmission = 0.0125f;

        [SerializeField]
        private Image m_MeterFill;

        private void OnEnable()
        {
            UpdateView();

            m_Emit = Emit;
            DistanceEmissionSystem.OnEmitted -= m_Emit;
            DistanceEmissionSystem.OnEmitted += m_Emit;
        }

        private void OnDisable()
        {
            DistanceEmissionSystem.OnEmitted -= m_Emit;
        }

        private void Emit(TravelerData traveler, ItemType emissionType)
        {
            if (m_EmissionType != emissionType)
                return;

            m_Quantity += m_QuantityPerEmission;
            UpdateView();
        }

        private void UpdateView()
        {
            m_MeterFill.fillAmount = m_Quantity;
        }
    }
}
