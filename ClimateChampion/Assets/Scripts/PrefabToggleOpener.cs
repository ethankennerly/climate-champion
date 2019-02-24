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

        public void Spawn(int index)
        {
            m_SelectedIndex = index;
            if (m_SpawnedInstance != null)
                Destroy(m_SpawnedInstance);
            GameObject nextSpawn = index >= m_OptionsToSpawn.Length ? null : m_OptionsToSpawn[index];
            if (nextSpawn == null)
                return;

            Instantiate(nextSpawn, transform);
        }
    }
}
