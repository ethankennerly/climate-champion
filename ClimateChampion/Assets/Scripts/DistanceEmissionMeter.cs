using System;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class DistanceEmissionMeter : MonoBehaviour
    {
        public static event Action<ItemType> OnFull;

        private Action<TravelerData, ItemType> m_Emit;

        [SerializeField]
        private ItemType m_EmissionType;

        [SerializeField]
        private float m_Capacity = 1f;

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
            if (m_Quantity >= m_Capacity)
                if (OnFull != null)
                    OnFull(m_EmissionType);

            UpdateView();
        }

        private void UpdateView()
        {
            m_MeterFill.fillAmount = m_Quantity / m_Capacity;
        }
    }
}
