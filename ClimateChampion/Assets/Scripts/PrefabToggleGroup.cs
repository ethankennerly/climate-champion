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
        private PrefabToggle[] m_Toggles;

        public void Open(PrefabToggleOpener spawnSite)
        {
            m_SpawnSite = spawnSite;
            PopulateToggles(spawnSite.OptionsToSpawn, spawnSite.SelectedIndex, m_Toggles);
            gameObject.SetActive(true);
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
