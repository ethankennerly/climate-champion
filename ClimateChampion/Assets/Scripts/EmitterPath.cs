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


    }
}
