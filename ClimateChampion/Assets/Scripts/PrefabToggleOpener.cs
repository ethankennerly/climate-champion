using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class PrefabToggleOpener : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_SpawnedInstance;

        [SerializeField]
        private TMP_Text m_SpawnedLabel;

        [SerializeField]
        private GameObject m_SelectedCallout;

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

        private void Start()
        {
            Close();
        }

        private void OnEnable()
        {
            if (m_Group == null)
                m_Group = PrefabToggleGroup.Instance;
        }

        public void Open()
        {
            m_Group.Open(this);
            m_SelectedCallout.SetActive(true);
            if (m_SpawnedInstance != null)
            {
                m_SpawnedLabel.text = m_SpawnedInstance.name;
            }
        }

        public void Close()
        {
            m_SelectedCallout.SetActive(false);
            m_SpawnedLabel.text = "";
        }

        public void Spawn(int index)
        {
            if (m_SelectedIndex == index)
                return;

            m_SelectedIndex = index;
            float remainingTime = 0f;
            TimedEmitter emitter;
            if (m_SpawnedInstance != null)
            {
                emitter = m_SpawnedInstance.GetComponentInChildren<TimedEmitter>();
                if (emitter != null)
                    remainingTime = emitter.RemainingTime;

                Destroy(m_SpawnedInstance);
            }

            GameObject nextPrefab = index >= m_OptionsToSpawn.Length ? null : m_OptionsToSpawn[index];
            if (nextPrefab == null)
                return;

            m_SpawnedInstance = Instantiate(nextPrefab, transform);
            emitter = m_SpawnedInstance.GetComponentInChildren<TimedEmitter>();
            if (emitter != null)
                emitter.RemainingTime = remainingTime;
        }
    }
}
