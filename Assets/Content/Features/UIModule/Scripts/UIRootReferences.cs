using Content.Features.CameraModule;
using UnityEngine;
using Zenject;

namespace Content.Features.UIModule.Scripts
{
    public class UIRootReferences : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform _healthBarsParent;
        [SerializeField] private Transform _damagePopupsParent;
        [SerializeField] private Transform _toolTipsParent;
        [SerializeField] private Canvas _canvas;
        private PlayerCameraModel _cameraModel;

        public Transform HealthBarsParent => _healthBarsParent;
        public Transform DamagePopupsParent => _damagePopupsParent;
        public Transform ToolTipsParent => _toolTipsParent;

        [Inject]
        public void InjectDependencies(PlayerCameraModel cameraModel)
        {
            _cameraModel = cameraModel;
        }

        public void Initialize()
        {
            _canvas.worldCamera = _cameraModel.CurrentCamera;
        }
    }
}