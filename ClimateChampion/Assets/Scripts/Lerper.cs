using UnityEngine;

namespace FineGameDesign.Utils
{
    public sealed class Lerper : MonoBehaviour
    {
        [SerializeField]
        private Transform m_BeginPosition;

        [SerializeField]
        private Transform m_EndPosition;

        public void Interpolate(float t)
        {
            transform.position = Vector3.Lerp(m_BeginPosition.position, m_EndPosition.position, t);
        }
    }
}
