using System;
using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class EmitterPath : MonoBehaviour
    {
        [Header("Traveler follows path then self-destructs.")]
        [SerializeField]
        private TravelerView m_Traveler;

        [Header("At site, traveler emits package; package emits content.")]
        [SerializeField]
        private TimedEmitter m_TimedEmitterPackage;

        [Serializable]
        public struct Site
        {
            [Header("Pause here for package to emit.")]
            public Transform waypoint;
            [Header("Package will emit this content.")]
            public GameObject packageContent;
        }

        [Header("Emits package at each waypoint.")]
        [SerializeField]
        private Site[] m_Path;
        private int m_PathIndex;

        private Action<TravelerData> m_OnArrived;

        private void OnEnable()
        {
            if (m_OnArrived == null)
                m_OnArrived = EmitPackage;

            MoveForwardSystem.OnArrived -= m_OnArrived;
            MoveForwardSystem.OnArrived += m_OnArrived;

            m_Traveler.Init();
            SetDestination(m_PathIndex);
        }

        private void OnDisable()
        {
            MoveForwardSystem.OnArrived -= m_OnArrived;
        }

        private void EmitPackage(TravelerData traveler)
        {
            if (traveler != m_Traveler.Data)
                return;

            GameObject package = (GameObject)Instantiate(m_TimedEmitterPackage.gameObject,
                traveler.position, Quaternion.identity);
            TimedEmitter emitterInPackage = package.GetComponent<TimedEmitter>();
            Site site = m_Path[m_PathIndex];
            emitterInPackage.PrefabToSpawn = site.packageContent;

            SetDestination(++m_PathIndex);
        }

        private void SetDestination(int pathIndex)
        {
            if (pathIndex >= m_Path.Length)
            {
                m_Traveler.Data.speed = 0f;
                // Destroy(gameObject);
                return;
            }

            Site site = m_Path[pathIndex];
            TravelerView.SetDestination(m_Traveler.Data, site.waypoint.position);
        }
    }
}
