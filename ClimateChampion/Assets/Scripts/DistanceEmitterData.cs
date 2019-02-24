using System;

namespace FineGameDesign.Utils
{
    [Serializable]
    public class DistanceEmitterData
    {
        public ItemType emissionType;
        public float rateOverDistance;
        public float remainingDistance;
        public bool emitted;
        public bool emissionDestroysTraveler;
    }
}
