using TMPro;
using UnityEngine;

namespace Common
{

    public class TextComponent : MonoBehaviour
    {
        public TextMeshProUGUI Text => _text;
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private bool ClearOnAwake = true;

        /// <summary>
        /// Textオブジェクト自体のTransform
        /// </summary>
        public Transform TextTransform => _text.transform;

        private void Awake()
        {
            if (ClearOnAwake) ClearText();
        }

        public void SetString(string s)
        {
            _text.text = s;
        }

        public void ClearText()
        {
            _text.text = "";
        }

        public void SetColor(Color32 color)
        {
            _text.color = color;
        }
    }
}