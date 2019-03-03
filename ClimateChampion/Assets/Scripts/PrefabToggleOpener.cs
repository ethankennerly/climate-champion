using System;
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
        private PrefabToggleGroup Group
        {
            get
            {
                if (m_Group == null)
                    m_Group = PrefabToggleGroup.Instance;
                if (m_Group == null)
                    Debug.LogWarning("Expected Group was defined.", context: this);
                return m_Group;
            }
        }

        private void Start()
        {
            Close();
        }

        public void Open()
        {
            if (Group == null)
                return;

            Group.Open(this);
            m_SelectedCallout.SetActive(true);
            if (m_SpawnedInstance != null)
                m_SpawnedLabel.text = m_SpawnedInstance.name;
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
            {
                m_SpawnedLabel.text = "";
                return;
            }

            m_SpawnedInstance = Instantiate(nextPrefab, transform);
            m_SpawnedInstance.name = TrimIfEndsWith(m_SpawnedInstance.name, "(Clone)");
            if (m_SpawnedInstance != null)
                m_SpawnedLabel.text = m_SpawnedInstance.name;
            emitter = m_SpawnedInstance.GetComponentInChildren<TimedEmitter>();
            if (emitter != null)
                emitter.RemainingTime = remainingTime;
        }

        private static string TrimIfEndsWith(string text, string suffix)
        {
            int end = text.LastIndexOf(suffix, StringComparison.Ordinal);
            if (end < 0)
                return text;

            return text.Substring(0, end);
        }
    }
}
