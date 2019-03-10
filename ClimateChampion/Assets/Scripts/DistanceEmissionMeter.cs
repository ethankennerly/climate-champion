using System;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class DistanceEmissionMeter : MonoBehaviour
    {
        public static event Action<ItemType> OnFull;

        public event Action<float> OnFillAmountUpdated;

        [Serializable]
        public struct Measurable
        {
            public ItemType type;
            public float change;
        }

        [SerializeField]
        private float m_Capacity = 200f;

        [SerializeField]
        private float m_Quantity;

        [SerializeField]
        private Image m_MeterFill;

        [SerializeField]
        public Measurable[] m_Measurables;

        private Action<TravelerData, ItemType> m_Emit;

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
            float change = GetChange(emissionType);
            if (change == 0f)
                return;

            m_Quantity += change;
            if (m_Quantity >= m_Capacity)
                if (OnFull != null)
                    OnFull(emissionType);

            UpdateView();
        }

        private float GetChange(ItemType emissionType)
        {
            foreach (Measurable measurable in m_Measurables)
            {
                if (emissionType != measurable.type)
                    continue;

                return measurable.change;
            }
            return 0f;
        }

        public void UpdateView()
        {
            float fillAmount = m_Quantity / m_Capacity;
            m_MeterFill.fillAmount = fillAmount;

            if (OnFillAmountUpdated != null)
                OnFillAmountUpdated(fillAmount);
        }
    }
}
