using Content.Features.AIModule.Scripts.Entity.EntityContext;
using Content.Features.EntityAnimatorModule.Scripts;
using Content.Features.PlayerData.Scripts.Datas;

namespace Content.Features.AIModule.Scripts.Entity.MonoEntity
{
    public class SellerMonoEntity : MonoEntity<SellerEntityContext>
    {
        protected override void InitializeContext()
        {
            base.InitializeContext();
            if (_entityContext.EntityAnimator == null)
                _entityContext.EntityAnimator = GetComponentInChildren<EntityAnimator>();
            _entityContext.EntityData = _entityDataService.GetEntityData<SellerEntityData>();
        }
    }
}