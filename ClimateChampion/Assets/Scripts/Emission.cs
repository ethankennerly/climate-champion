using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class Emission : MonoBehaviour
    {
        [SerializeField]
        private ItemType m_Type;
        public ItemType Type
        {
            get { return m_Type; }
        }

        public static event Action<Emission> OnEnabled;

        private void OnEnable()
        {
            if (OnEnabled != null)
                OnEnabled(this);
        }
    }
}
