using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class PrefabToggleGroup : MonoBehaviour
    {
        public static PrefabToggleGroup Instance { get; private set; }

        [SerializeField]
        private PrefabToggleOpener m_SpawnSite;

        [SerializeField]
        private PrefabToggle[] m_Toggles;

        [SerializeField]
        private Transform m_FillRoot;

        [SerializeField]
        private Image m_FillImage;

        private void Start()
        {
            Close();
        }

        private void OnEnable()
        {
            Instance = this;
        }

        public void Open(PrefabToggleOpener spawnSite)
        {
            Close();

            m_SpawnSite = spawnSite;
            PopulateToggles(spawnSite.OptionsToSpawn, spawnSite.SelectedIndex, m_Toggles);
            AddProgressListener(spawnSite, m_Toggles);
            gameObject.SetActive(true);
        }

        public void Close()
        {
            if (m_SpawnSite != null)
            {
                RemoveProgressListener(m_SpawnSite, m_Toggles);
                m_SpawnSite.Close();
            }
            gameObject.SetActive(false);
        }

        private void PopulateToggles(GameObject[] optionsToSpawn, int selectedIndex,
            PrefabToggle[] toggles)
        {
            int index = 0;
            int numOptions = optionsToSpawn.Length;
            foreach (PrefabToggle toggle in toggles)
            {
                GameObject optionToSpawn = index >= numOptions ? null : optionsToSpawn[index];
                if (optionToSpawn == null)
                {
                    toggle.gameObject.SetActive(false);
                    index++;
                    continue;
                }
                toggle.Toggle.onValueChanged.RemoveListener(Spawn);
                bool isOn = index == selectedIndex;
                toggle.Toggle.isOn = isOn;
                toggle.Toggle.onValueChanged.AddListener(Spawn);
                toggle.Label.text = optionToSpawn.name;
                SpriteRenderer renderer = optionToSpawn.GetComponentInChildren<SpriteRenderer>();
                toggle.Preview.sprite = renderer.sprite;
                toggle.gameObject.SetActive(true);
                index++;
            }
        }

        private Action<float> m_OnProgressChanged;

        private void AddProgressListener(PrefabToggleOpener spawnSite, PrefabToggle[] prefabToggles)
        {
            TimedEmitter emitter = spawnSite.FindEmitter();
            if (emitter == null)
                return;

            UpdateFill(emitter.Progress);

            if (m_OnProgressChanged == null)
                m_OnProgressChanged = UpdateFill;

            emitter.OnProgressChanged -= m_OnProgressChanged;
            emitter.OnProgressChanged += m_OnProgressChanged;

            if (m_FillRoot == null)
                return;

            PrefabToggle selectedToggle = prefabToggles[spawnSite.SelectedIndex];
            m_FillRoot.SetParent(selectedToggle.transform);
        }

        private void RemoveProgressListener(PrefabToggleOpener spawnSite, PrefabToggle[] prefabToggles)
        {
            TimedEmitter emitter = spawnSite.FindEmitter();
            if (emitter == null)
                return;

            emitter.OnProgressChanged -= m_OnProgressChanged;
        }

        private void UpdateFill(float progress)
        {
            if (m_FillImage == null)
                return;

            m_FillImage.fillAmount = progress;
        }

        private void Spawn(bool selected)
        {
            if (!selected)
                return;

            for (int index = 0, numToggles = m_Toggles.Length; index < numToggles; ++index)
            {
                PrefabToggle toggle = m_Toggles[index];
                if (!toggle.gameObject.activeSelf)
                    continue;

                if (!toggle.Toggle.isOn)
                    continue;

                m_SpawnSite.Spawn(index);
            }

            Close();
        }
    }
}
