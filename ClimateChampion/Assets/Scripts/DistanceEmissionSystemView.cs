using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class DistanceEmissionSystemView : ASingletonView<DistanceEmissionSystem>
    {
        private Action<TravelerData, ItemType> m_Emit;

        [Serializable]
        private struct Emission
        {
            public ItemType emissionType;
            public GameObject prefabToSpawn;
        }

        [SerializeField]
        private Emission[] m_Emissions;

        private void OnEnable()
        {
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
            foreach (Emission emission in m_Emissions)
            {
                if (emissionType != emission.emissionType)
                    continue;

                Instantiate(emission.prefabToSpawn,
                    new Vector3(traveler.position.x, traveler.position.y, 0f),
                    Quaternion.identity
                );
            }
        }
    }
}
