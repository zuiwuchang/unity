using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using System;
using UnityEngine.Events;
namespace com.king011
{
    [Serializable]
    /// <summary>
    /// Function definition for a switch state change event.
    /// </summary>
    public class SwitchChangeEvent : UnityEvent<bool> { }

    [AddComponentMenu("UI-K/Switch", 0)]
    public class Switch : Selectable, IPointerClickHandler
    {
        public class State
        {
            public bool opened = false;
            public bool completed = false;
            public Vector3 speed;
            public Vector3 target = new Vector3(0, 0, 0);
        }
        protected Switch() { }

        [FormerlySerializedAs("onCompleted")]
        [SerializeField]
        private SwitchChangeEvent _completed = new SwitchChangeEvent();
        [FormerlySerializedAs("onChanged")]
        [SerializeField]
        private SwitchChangeEvent _changed = new SwitchChangeEvent();

        [Label("動畫時間")]
        public float duration = 0.25f;
        [Label("是否選中")]
        public bool opened = false;
        private State state = new State();

        private RectTransform rectTransform_;
        public RectTransform rectTransform
        {
            get
            {
                if (rectTransform_ == null)
                {
                    rectTransform_ = GetComponent<RectTransform>();
                }
                return rectTransform_;
            }
        }
        private RectTransform knobRectTransform_;
        public RectTransform knobRectTransform
        {
            get
            {
                if (knobRectTransform_ == null)
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        var child = transform.GetChild(i);
                        if (child.name == "Knob")
                        {
                            knobRectTransform_ = child.GetComponent<RectTransform>();
                            if (knobRectTransform_ != null)
                            {
                                break;
                            }
                        }
                    }
                }
                return knobRectTransform_;
            }
        }

        // 點擊事件 切換 狀態
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            opened =  !opened;
            _changed.Invoke(opened);
        }
        private void Update()
        {
            UpdateKnob();
        }
        private void UpdateKnob()
        {
            RectTransform knob;
            if (state.opened != opened) // 狀態變化 重置動畫
            {
                state.opened = opened;
                knob = knobRectTransform;
                if (knob == null)
                {
                    Debug.LogWarning($"Switch not found Knob");
                    state.completed = true;
                    return;
                }
                state.completed = false;

                var x = rectTransform.rect.width / 2;
                if (opened)
                {
                    state.target.x = x;
                }
                else
                {
                    state.target.x = -x;
                }
#if UNITY_EDITOR
            if (!Application.isPlaying) // 在 編輯器中 且沒有運行遊戲
            {
                // 跳過動畫直接到最終狀態
                state.completed = true;
                knob.localPosition = state.target;

                 _completed.Invoke(opened);
                return;
            }
#endif
                state.speed = (state.target - knob.localPosition) / duration;
            }
            else if (state.completed)
            {
                return;
            }
            else
            {
                knob = knobRectTransform;
                if (knob == null)
                {
                    Debug.LogWarning($"Switch not found Knob");
                    state.completed = true;
                    return;
                }
            }
            // 移動 knob

            var move = state.speed * Time.deltaTime;
            var target = knob.localPosition + move;

            if ((move.x > 0 && target.x >= state.target.x) ||
                (move.x < 0 && target.x <= state.target.x))
            {
                target.x = state.target.x;
                target.y = 0;
                target.z = 0;
                state.completed = true;
                _completed.Invoke(opened);
            }
            else
            {
                if ((move.y > 0 && target.y > 0) || (move.y < 0 && target.y < 0))
                {
                    target.y = 0;
                }
                if ((move.z > 0 && target.z > 0) || (move.z < 0 && target.z < 0))
                {
                    target.z = 0;
                }
            }

            knob.localPosition = target;
        }
    }
}