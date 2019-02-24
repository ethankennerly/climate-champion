using System.Collections.Generic;
using UnityEngine;

namespace FineGameDesign.Utils
{
    /// <summary>
    /// - Prefab Toggle Opener
    ///     - Spawned instance
    ///     - List of prefabs
    ///     - Selected index
    /// - Prefab Toggle Group
    ///     - Prefab Toggle Opener
    ///     - Toggles
    /// </summary>
    public sealed class PrefabToggleOpener : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_SpawnedInstance;

        [SerializeField]
        private GameObject[] m_OptionsToSpawn;

        [SerializeField]
        private int m_SelectedIndex;

        // TODO:
        // [SerializeField]
        // private PrefabToggleGroup m_Group;

        public void OpenToggleGroup()
        {
        }
    }
}
