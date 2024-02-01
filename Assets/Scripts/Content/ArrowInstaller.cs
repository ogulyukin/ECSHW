using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class ArrowInstaller : EntityInstaller
    {
        [SerializeField] private int damage;
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime = 3f;
        protected override void Install(Entity entity)
        {
            var tr = transform;
            var forward = tr.forward;
            entity.AddData(new WeaponTag());
            entity.AddData(new Position{value = tr.position});
            entity.AddData(new MoveDirection{value = forward});
            entity.AddData(new AttackDamage{value = damage});
            entity.AddData(new MoveSpeed{value = speed});
            entity.AddData(new TransformView{value = transform});
            entity.AddData(new TargetPosition{value = forward});
            entity.AddData(new Team());
            entity.AddData(new LifeTime{value = lifeTime});
            entity.AddData(new LifeTimeCurrent{value = lifeTime});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
