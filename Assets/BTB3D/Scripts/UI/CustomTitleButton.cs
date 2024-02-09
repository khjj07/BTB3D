using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BTB3D.Scripts.UI
{
    public class CustomTitleButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private Color _normalColor;
        private Color _normalTextColor;
        [SerializeField] private Color highlightColor;
        [SerializeField] private Color highlightTextColor;
        [SerializeField] private Color clickedColor;
        [SerializeField] private Color clickedTextColor;

       public delegate void OnClickDelegate();
        public OnClickDelegate onClickDelegate;

        private Image _backImage;
        private TextMeshProUGUI _text;
        private bool _isClicked;

        public void Awake()
        {
            _backImage = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _normalColor = _backImage.color;
            if (_text)
            {
                _normalTextColor = _text.color;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _backImage.color = highlightColor;
            if(_text)
                _text.color = highlightTextColor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isClicked = true;
            _backImage.color = clickedColor;
            if (_text)
                _text.color = clickedTextColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isClicked = false;
            _backImage.color = _normalColor;
            if (_text)
                _text.color = _normalTextColor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isClicked)
            {
                _isClicked = false;
                _backImage.color = _normalColor;
                if (_text)
                    _text.color = _normalTextColor;
                onClickDelegate();
            }
           
        }
    }
}
