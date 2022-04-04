using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    public class UiInformerScript : MonoBehaviour
    {
        private const string _uiInformerText = "E";

        private Text _textField;
        private Canvas _canvas;

        private void Start()
        {
            //TODO: Extract to external script
            _canvas = GetComponentInParent<Canvas>();
            _canvas.worldCamera = Camera.main;
            //-----------------------------------

            _textField = GetComponent<Text>();
            _textField.text = _uiInformerText;
            Disappear(0f);
        }

        public void Appear(float duration)
        {
            _textField.CrossFadeAlpha(1f, duration, false);
        }

        public void Disappear(float duration)
        {
            _textField.CrossFadeAlpha(0f, duration, false);
        }
    }
}
