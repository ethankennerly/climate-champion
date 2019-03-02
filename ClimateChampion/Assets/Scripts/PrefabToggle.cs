using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FineGameDesign.Utils
{
    public sealed class PrefabToggle : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text m_Label;
        public TMP_Text Label
        {
            get { return m_Label; }
        }

        [SerializeField]
        private Toggle m_Toggle;
        public Toggle Toggle
        {
            get { return m_Toggle; }
        }

        [SerializeField]
        private Image m_Preview;
        public Image Preview
        {
            get { return m_Preview; }
        }
    }
}
