using System;
using DG.Tweening;
using UnityEngine;

namespace Content.Features.UIModule.Scripts.UIAnimations
{
    public class PopupWindowAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform _window;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _offsetY = 300f;
        
        public event Action OnShowStart;
        public event Action OnHideStart;
        public event Action OnShowComplete;
        public event Action OnHideComplete;

        private Vector2 _targetPosition;

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
        }

        public void Show()
        {
            _canvasGroup.alpha = 0f;
            _window.anchoredPosition = _targetPosition + Vector2.up * _offsetY;
            OnShowStart?.Invoke();
            Sequence seq = DOTween.Sequence();
            seq.Join(_window.DOAnchorPos(_targetPosition, _duration).SetEase(Ease.OutBack));
            seq.Join(_canvasGroup.DOFade(1f, _duration));
            seq.OnComplete(() => { OnShowComplete?.Invoke(); });
            seq.Play();
        }

        public void HideAndDeactivate()
        {
            Sequence seq = DOTween.Sequence();
            seq.Join(_window.DOAnchorPos(_targetPosition + Vector2.up * _offsetY, _duration).SetEase(Ease.InBack));
            seq.Join(_canvasGroup.DOFade(0f, _duration));

            seq.OnComplete(() => { OnHideComplete?.Invoke(); });
            OnHideStart?.Invoke();
            seq.Play();
        }
    }
}