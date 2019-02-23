using System;

namespace FineGameDesign.Utils
{
    public sealed class DistanceEmissionSystem : ASingleton<DistanceEmissionSystem>
    {
        public static event Action<TravelerData, ItemType> OnEmitted;

        public DistanceEmissionSystem()
        {
            DistanceEmitterView.OnDistanceChanged -= UpdateEmission;
            DistanceEmitterView.OnDistanceChanged += UpdateEmission;
        }

        ~DistanceEmissionSystem()
        {
            DistanceEmitterView.OnDistanceChanged -= UpdateEmission;
        }

        /// <summary>
        /// Max one emission per update.
        /// </summary>
        public void UpdateEmission(DistanceEmitterData emitter, TravelerData traveler)
        {
            if (emitter.emissionType == ItemType.Unknown)
                return;

            emitter.remainingDistance += traveler.deltaDistance;
            if (emitter.rateOverDistance < emitter.remainingDistance)
                return;

            emitter.rateOverDistance -= emitter.remainingDistance;

            if (OnEmitted != null)
                OnEmitted(traveler, emitter.emissionType);
        }
    }
}