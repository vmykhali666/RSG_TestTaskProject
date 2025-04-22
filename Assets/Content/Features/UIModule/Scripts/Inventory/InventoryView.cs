using System;
using System.Collections.Generic;
using Content.Features.AIModule.Scripts.Entity;
using Content.Features.StorageModule.Scripts;
using Content.Features.UIModule.Scripts.UIAnimations;
using UnityEngine;
using UnityEngine.UI.Extensions;
using Zenject;

namespace Content.Features.UIModule.Scripts.Inventory
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

        private List<InventoryItem> _spawnedItems = new();
        private List<GameObject> _spawnedCells = new();
        private StorageSettings _storageSettings;

        public bool IsOpened { get; private set; }
        public event Action<InventoryItem> OnItemRemoved;

        [Inject]
        public void InjectDependencies(StorageSettings storageSettings)
        {
            _storageSettings = storageSettings;
        }

        public void Initialize()
        {
            ConfigureReorderableList();
            ConfigurePopupAnimator();
            InitializeViewState();
            SetCells();
        }

        private void ConfigureReorderableList()
        {
            _reorderableList.maxItems = _storageSettings.MaxItemsQuantity;
            _reorderableList.OnElementRemoved.AddListener(OnItemRemove);
        }

        private void ConfigurePopupAnimator()
        {
            _popupWindowAnimator.OnHideComplete += OnHideComplete;
            _popupWindowAnimator.OnShowStart += OnShowStart;
            _popupWindowAnimator.OnShowComplete += OnShowComplete;
        }

        private void InitializeViewState()
        {
            if (IsOpened)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void OnShowComplete() => IsOpened = true;

        private void OnShowStart() => gameObject.SetActive(true);

        private void OnHideComplete()
        {
            gameObject.SetActive(false);
            IsOpened = false;
        }

        public void Show()
        {
            if (!IsOpened)
            {
                _popupWindowAnimator.Show();
            }
        }

        public void Hide()
        {
            if (IsOpened)
            {
                _popupWindowAnimator.HideAndDeactivate();
            }
        }

        private void OnItemRemove(ReorderableList.ReorderableListEventStruct args)
        {
            Debug.Log($"Removed item at index {args.FromIndex}");
            // OnItemRemoved?.Invoke(_spawnedItems[args.FromIndex]);
        }

        public void SetItems(List<Item> items)
        {
            ClearItems();
            items.ForEach(CreateItem);
        }

        private void CreateItem(Item item)
        {
            var newItem = Instantiate(_itemPrefab, _itemsParent);
            newItem.SetItem(item);
            _spawnedItems.Add(newItem);
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
                var cellObject = Instantiate(_cellPrefab, _cellsParent);
                _spawnedCells.Add(cellObject);
            }
        }

        private void ClearCells()
        {
            _spawnedCells.ForEach(Destroy);
            _spawnedCells.Clear();
        }

        private void OnDestroy()
        {
            _reorderableList.OnElementRemoved.RemoveListener(OnItemRemove);
            _popupWindowAnimator.OnHideComplete -= OnHideComplete;
            _popupWindowAnimator.OnShowStart -= OnShowStart;
        }
    }
}