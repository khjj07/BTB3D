using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BTB3D.Scripts.UI
{
    public class TabButton : MonoBehaviour, IPointerClickHandler
    {
        public Color normalColor;
        public Color selectColor;
        private Image _image;
        public bool _isSelected=false;
        public UnityEvent onClickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            onClickEvent.Invoke();
        }

        public void Start()
        {
            _image = GetComponent<Image>();
            Unselect();
        }

        public void Select()
        {
            _image.color = selectColor;
            _isSelected = true;
        }

        public void Unselect()
        {
            _image.color = normalColor;
            _isSelected = false;
        }
    }
}
