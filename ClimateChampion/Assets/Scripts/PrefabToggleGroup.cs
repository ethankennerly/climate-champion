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
            m_SpawnSite = spawnSite;
            PopulateToggles(spawnSite.OptionsToSpawn, spawnSite.SelectedIndex, m_Toggles);
            gameObject.SetActive(true);
        }

        public void Close()
        {
            if (m_SpawnSite != null)
                m_SpawnSite.Close();
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
                toggle.Toggle.isOn = index == selectedIndex;
                toggle.Toggle.onValueChanged.AddListener(Spawn);
                toggle.Label.text = optionToSpawn.name;
                SpriteRenderer renderer = optionToSpawn.GetComponentInChildren<SpriteRenderer>();
                toggle.Preview.sprite = renderer.sprite;
                toggle.gameObject.SetActive(true);
                index++;
            }
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
        }
    }
}
