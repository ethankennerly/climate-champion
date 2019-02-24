using System;

namespace FineGameDesign.Utils
{
    /// <remarks>
    /// - Overengineered Distance Emitter.
    ///     - Timed Emitter simpler than Distance Emitter.
    ///     - Decoupling unnecessarily added complexity.
    ///     - Could have used timed emitter instead of distance emitter.
    ///         - Since time and speed converts into distance.
    /// </remarks>
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
        private void UpdateEmission(DistanceEmitterData emitter, TravelerData traveler)
        {
            if (emitter.emissionType == ItemType.Unknown)
                return;

            emitter.remainingDistance += traveler.deltaDistance;
            if (emitter.rateOverDistance > emitter.remainingDistance)
                return;

            emitter.remainingDistance -= emitter.rateOverDistance;

            emitter.numEmissions++;

            if (OnEmitted != null)
                OnEmitted(traveler, emitter.emissionType);
        }
    }
}
