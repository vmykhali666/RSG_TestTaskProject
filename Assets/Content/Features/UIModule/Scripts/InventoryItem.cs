using System;
using Content.Features.StorageModule.Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.UIModule.Scripts
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _newItemIndicator;
        public Item Item { get; private set; }

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
        }

        private void OnDisable()
        {
            transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        }

        public void SetItem(Item item, bool isNewItem = false)
        {
            Item = item;
            _icon.sprite = item.Icon;
            SetNewItemIndicator(isNewItem);
        }

        public void SetNewItemIndicator(bool isActive)
        {
            _newItemIndicator.SetActive(isActive);
        }

        public void OnDestroy()
        {
            transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => { Destroy(gameObject); });
        }
    }
}