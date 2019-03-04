using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FineGameDesign.Utils
{
    /// <summary>
    /// TODO:
    /// 1. [ ] Celebrate reducing CO2.
    ///     1. [ ] With change in CO2 meter, deal shuffled tiles.
    ///         1. [ ] Green grass to yellow sand.
    ///         1. [ ] Green plants to empty.
    ///         1. [ ] Green trees to barren trees.
    /// </summary>
    public sealed class TilemapMixer : MonoBehaviour
    {
        [Serializable]
        public struct MixInput
        {
            public float lowerBound;
            [Header("Hides tile if empty.")]
            public Tilemap map;
        }

        [SerializeField]
        private Tilemap m_MapToEdit;

        [SerializeField]
        private MixInput[] m_MixInputs;

        [SerializeField]
        private float m_Mix;
        public float Mix
        {
            get { return m_Mix; }
            set
            {
                if (m_Mix == value)
                    return;

                SetMix(value);
            }
        }

        private void OnValidate()
        {
            SetMix(m_Mix);
        }

        private void SetMix(float nextMix)
        {
            m_Mix = nextMix;
        }
    }
}
