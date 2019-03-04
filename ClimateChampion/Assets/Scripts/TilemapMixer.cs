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

            public List<Vector3Int> cellsToMix;

            public int numActive;
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

            int numInputs = m_MixInputs.Length;
            if (numInputs == 0)
                return;

            MixInput lowerInput = m_MixInputs[0];
            for (int inputIndex = 1; inputIndex < numInputs;
                lowerInput = m_MixInputs[inputIndex], ++inputIndex
            )
            {
                MixInput input = m_MixInputs[inputIndex];
                int previousNumActive = input.numActive;
                int nextNumActive = StableLerp(lowerInput.lowerBound, input.lowerBound, nextMix,
                    0, input.cellsToMix.Count, previousNumActive);
                if (nextNumActive == input.numActive)
                    continue;

                if (nextNumActive > previousNumActive)
                {
                    for (int activeIndex = previousNumActive; activeIndex < nextNumActive; ++activeIndex)
                    {
                        Vector3Int cell = input.cellsToMix[activeIndex];
                        SetTile(input, cell);
                    }
                }
                else
                {
                    for (int activeIndex = previousNumActive - 1; activeIndex >= nextNumActive; --activeIndex)
                    {
                        Vector3Int cell = input.cellsToMix[activeIndex];
                        SetTile(lowerInput, cell);
                    }
                }
                input.numActive = nextNumActive;
            }
        }

        private void SetTile(MixInput input, Vector3Int cell)
        {
            TileBase tile = input.map == null ? null : input.map.GetTile(cell);
            m_MapToEdit.SetTile(cell, tile);
        }

        private void Init()
        {
            if (m_Initialized)
                return;
            m_Initialized = true;

            foreach (MixInput input in m_MixInputs)
            {
                input.cellsToMix = GetOccupiedCells(input.map);
                Deck.ShuffleList(input.cellsToMix);
            }
        }

        private List<Vector3Int> GetOccupiedCells(Tilemap map)
        {
            List<Vector3Int> cells = new List<Vector3Int>();
            if (map == null)
                return cells;

            BoundsInt bounds = map.cellBounds;
            foreach (Vector3Int cell in bounds.allPositionsWithin)
            {
                if (!map.HasTile(cell))
                    continue;

                Sprite sprite = map.GetSprite(cell);
                if (sprite == null)
                    continue;

                cells.Add(cell);
            }
            return cells;
        }

        private static int StableLerp(float min, float max, float mix,
            int minOut, int maxOut, int previousOut)
        {
            float t = Mathf.InverseLerp(min, max, mix);
            float nextOut = Mathf.Lerp(minOut, maxOut, t);
            float distance = Mathf.Abs(nextOut - previousOut);
            if (distance < 1f)
                return previousOut;
            return (int)Mathf.Round(nextOut);
        }
    }
}
