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
        public GameObject[] OptionsToSpawn
        {
            get { return m_OptionsToSpawn; }
        }

        [SerializeField]
        private int m_SelectedIndex;
        public int SelectedIndex
        {
            get { return m_SelectedIndex; }
            set { m_SelectedIndex = value; }
        }

        [SerializeField]
        private PrefabToggleGroup m_Group;

        public void Open()
        {
            m_Group.Open(this);
        }
    }
}
