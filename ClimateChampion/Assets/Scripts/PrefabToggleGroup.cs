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

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private static void PopulateToggles(GameObject[] optionsToSpawn, int selectedIndex,
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
                    continue;
                }
                toggle.Toggle.isOn = index == selectedIndex;
                toggle.Label.text = optionToSpawn.name;
                toggle.gameObject.SetActive(true);
                index++;
            }
        }
    }
}

