using System;
using Content.Features.UIModule.Scripts.UIAnimations;
using TMPro;
using UnityEngine;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class InventoryBadge : MonoBehaviour
    {
        [SerializeField] private GameObject _badge;
        [SerializeField] private TMP_Text _badgeText;
        [SerializeField] private PopAppearAnimator _appearAnimator;

        private int _count;

        public void Initialize()
        {
            _badge.SetActive(false);
            _badgeText.gameObject.SetActive(false);
            if (TryGetComponent<PopAppearAnimator>(out var animator))
            {
                _appearAnimator = animator;
            }

            _badgeText.GetComponent<TextMesh>();
            
            _appearAnimator.OnAppearComplete += OnAppearComplete;
            _appearAnimator.OnAppperStart += OnAppearStart;
        }

        private void OnAppearStart()
        {
            _badge.SetActive(true);
            _badgeText.gameObject.SetActive(true);
        }

        private void OnAppearComplete()
        {
            _badgeText.text = _count.ToString();
        }

        public void Show(int count)
        {
            _count = count;
            if (_appearAnimator == null)
            {
                OnAppearStart();
                OnAppearComplete();
            }
            else
            {
                _appearAnimator.Play();
            }
        }

        public void Hide()
        {
            _badge.SetActive(false);
            _badgeText.gameObject.SetActive(false);
        }
    }
}