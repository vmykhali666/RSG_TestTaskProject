using System;
using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.UIModule.Scripts.InventoryToggle
{
    public class InventoryToggleView : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Badge _badge;
        public event Action<bool> OnToggleChanged;

        public void Initialize()
        {
            _badge.Initialize();
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        public void SetToggleState(bool isOn)
        {
            _toggle.isOn = isOn;
        }
        
        private void OnValueChanged(bool isOn)
        {
            _toggle.isOn = isOn;
            OnToggleChanged?.Invoke(isOn);
        }

        public void ShowBadge(int count)
        {
            _badge.Show(count);
        }

        public void HideBadge()
        {
            _badge.Hide();
        }
    }
}