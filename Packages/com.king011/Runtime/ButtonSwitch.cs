using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System;
using UnityEngine.Events;
namespace com.king011
{
    [AddComponentMenu("UI-K/ButtonSwitch", 1)]
    public class ButtonSwitch : Selectable, IPointerClickHandler
    {
        protected ButtonSwitch() { }
        [Label("選中精靈")]
        public Sprite openedSprite;
        [Label("關閉精靈")]
        public Sprite closedSprite;

        [FormerlySerializedAs("onChanged")]
        [SerializeField]
        private SwitchChangeEvent _changed = new SwitchChangeEvent();
        [Label("是否選中")]
        public bool opened = false;

        private Image _image;
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            opened = !opened;
            _changed.Invoke(opened);
        }
        private void Update()
        {
            var image = _image;
            if (image == null)
            {
                _image = GetComponent<Image>();
                image = _image;
            }
            var sprite = opened ? openedSprite : closedSprite;
            if (image.sprite != sprite)
            {
                image.sprite = sprite;
            }
        }
    }
}