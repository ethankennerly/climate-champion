using System;
using System.Collections.Generic;
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
        public class MixInput
        {
            public float lowerBound;

            [Header("Hides tile if empty.")]
            public Tilemap map;

            public List<TileBase> tilesToMix;
        }

        [SerializeField]
        private Tilemap m_MapToEdit;

        [SerializeField]
        private MixInput[] m_MixInputs;

        private bool m_Initialized;

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

        private void OnEnable()
        {
            Init();
        }

        private void SetMix(float nextMix)
        {
            Init();
            m_Mix = nextMix;
        }

        private void Init()
        {
            if (m_Initialized)
                return;
            m_Initialized = true;

            foreach (MixInput input in m_MixInputs)
            {
                input.tilesToMix = GetTiles(input.map);
                Deck.ShuffleList(input.tilesToMix);
            }
        }

        private List<TileBase> GetTiles(Tilemap map)
        {
            List<TileBase> tiles = new List<TileBase>();
            if (map == null)
                return tiles;

            BoundsInt bounds = map.cellBounds;
            foreach (Vector3Int cell in bounds.allPositionsWithin)
            {
                if (!map.HasTile(cell))
                    continue;

                tiles.Add(map.GetTile(cell));
            }
            return tiles;
        }
    }
}
