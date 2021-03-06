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
            AddEmitterDestroyedListener(spawnSite);
            gameObject.SetActive(true);
        }

        public void Close()
        {
            if (m_SpawnSite != null)
            {
                RemoveProgressListener(m_SpawnSite);
                RemoveEmitterDestroyedListener(m_SpawnSite);
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

        #region UpdateFillOnProgressChanged

        [SerializeField]
        private Transform m_FillRoot;

        [SerializeField]
        private Image m_FillImage;

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
            m_FillRoot.SetParent(selectedToggle.transform, false);
        }

        private void RemoveProgressListener(PrefabToggleOpener spawnSite)
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

        #endregion UpdateFillOnProgressChanged

        #region CloseOnEmitterDestroyed

        private Action<TimedEmitter> m_OnEmitterDestroyed;

        private void AddEmitterDestroyedListener(PrefabToggleOpener spawnSite)
        {
            TimedEmitter emitter = spawnSite.FindEmitter();
            if (emitter == null)
            {
                Close();
                return;
            }

            if (m_OnEmitterDestroyed == null)
                m_OnEmitterDestroyed = CloseIfEmitterNamesAreEqual;

            TimedEmitter.OnDestroyed -= m_OnEmitterDestroyed;
            TimedEmitter.OnDestroyed += m_OnEmitterDestroyed;
        }

        private void RemoveEmitterDestroyedListener(PrefabToggleOpener spawnSite)
        {
            TimedEmitter emitter = spawnSite.FindEmitter();
            if (emitter == null)
                return;

            TimedEmitter.OnDestroyed -= m_OnEmitterDestroyed;
        }

        /// <summary>
        /// Strangely, the emitters appear equal but are different.
        /// </summary>
        private void CloseIfEmitterNamesAreEqual(TimedEmitter otherEmitter)
        {
            if (m_SpawnSite == null)
                return;

            TimedEmitter emitter = m_SpawnSite.FindEmitter();
            if (emitter == null)
                return;

            if (emitter.name != otherEmitter.name)
                return;

            Close();
        }

        #endregion CloseOnEmitterDestroyed
    }
}
