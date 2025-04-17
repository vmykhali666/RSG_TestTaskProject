using System;
using System.Collections.Generic;
using Content.Features.StorageModule.Scripts;
using Content.Features.UIModule.Scripts.UIAnimations;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class InventoryView : MonoBehaviour, IInitializable
    {
        [SerializeField] private InventoryItem _itemPrefab;
        [SerializeField] private GameObject _cellPrefab;

        [SerializeField] private Transform _itemsParent;
        [SerializeField] private Transform _cellsParent;

        [SerializeField] private ReorderableList _reorderableList;
        [SerializeField] private PopupWindowAnimator _popupWindowAnimator;
        [SerializeField] private bool _isOpened;

        private List<InventoryItem> _spawnedItems;
        private List<GameObject> _spawnedCells;

        private StorageSettings _storageSettings;
        
        public bool IsOpened { get => _isOpened; private set => _isOpened = value; }

        public event Action<InventoryItem> OnItemRemoved;

        [Inject]
        public void InjectDependencies(StorageSettings storageSettings)
        {
            _storageSettings = storageSettings;
        }

        public void Initialize()
        {
            _spawnedItems = new List<InventoryItem>();
            _spawnedCells = new List<GameObject>();
            _reorderableList.maxItems = _storageSettings.MaxItemsQuantity;
            _reorderableList.OnElementRemoved.AddListener(OnItemRemove);
            _popupWindowAnimator.OnHideComplete += OnHideComplete;
            _popupWindowAnimator.OnShowStart += OnShowStart;
            _popupWindowAnimator.OnShowComplete += OnShowComplete;
            if (IsOpened)
            {
                Show();
            }
            else
            {
                Hide();
            }

            SetCells();
        }

        private void OnShowComplete()
        {
            IsOpened = true;
        }

        private void OnShowStart()
        {
            gameObject.SetActive(true);
        }

        private void OnHideComplete()
        {
            gameObject.SetActive(false);
            IsOpened = false;
        }

        public void Show()
        {
            if (IsOpened)
            {
                return;
            }

            _popupWindowAnimator.Show();
        }

        public void Hide()
        {
            if (!IsOpened)
            {
                return;
            }

            _popupWindowAnimator.HideAndDeactivate();
        }

        private void OnItemRemove(ReorderableList.ReorderableListEventStruct arg0)
        {
            Debug.Log($"Removed item at index {arg0.FromIndex}");
            // OnItemRemoved?.Invoke(_spawnedItems[arg0.FromIndex]);
        }

        public void SetItems(List<Item> items)
        {
            ClearItems();
            
            items.ForEach(CreateItem);
        }

        private void CreateItem(Item item)
        {
            _itemPrefab.SetItem(item);
            InventoryItem itemItem = Instantiate(_itemPrefab, _itemsParent);
            _spawnedItems.Add(itemItem);
        }

        private void ClearItems()
        {
           _spawnedItems.ForEach(item => Destroy(item.gameObject));

            _spawnedItems.Clear();
        }

        private void SetCells()
        {
            ClearCells();
            for (int i = 0; i < _storageSettings.MaxItemsQuantity; i++)
            {
                GameObject cellObject = Instantiate(_cellPrefab, _cellsParent);
                _spawnedCells.Add(cellObject);
            }
        }

        private void ClearCells()
        {
            _spawnedCells.ForEach(Destroy);
            _spawnedCells.Clear();
        }

        public void OnDestroy()
        {
            _reorderableList.OnElementRemoved.RemoveListener(OnItemRemove);
            _popupWindowAnimator.OnHideComplete -= OnHideComplete;
            _popupWindowAnimator.OnShowStart -= OnShowStart;
        }
    }
}