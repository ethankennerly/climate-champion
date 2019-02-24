using System.Collections.Generic;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class PrefabToggleOpener : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_SpawnedInstance;

        [SerializeField]
        private GameObject[] m_OptionsToSpawn;

        [SerializeField]
        private int m_SelectedIndex;

        [SerializeField]
        private PrefabToggleGroup m_Group;

        public void Open()
        {
            m_Group.Open(this);
        }
    }
}
