using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class PrefabToggleGroup : MonoBehaviour
    {
        [SerializeField]
        private PrefabToggleOpener m_SpawnSite;

        [SerializeField]
        private ToggleGroup m_ToggleGroup;

        public void Open(PrefabToggleOpener spawnSite)
        {
            m_SpawnSite = spawnSite;
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}

