using Leopotam.EcsLite.Entities;
using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using EcsEngine.Views;
using UnityEngine;

namespace Content
{
    public sealed class SwordsmanInstaller : EntityInstaller
    {
        [SerializeField] private int team;
        [SerializeField] private float attackDistance;
        [SerializeField] private float attackTimeout;
        [SerializeField] private float attackActionTimeout;
        [SerializeField] private int health;
        [SerializeField] private int damage;
        [SerializeField] private float deathTimeout;
        [SerializeField] private Animator animator;
        [SerializeField] private UnitsAudioController unitsAudioController;
        [SerializeField] private VfxController vfxController;
        protected override void Install(Entity entity)
        {
            var position = transform.position;
            entity.AddData(new UnitTag());
            entity.AddData(new Position{value = position});
            entity.AddData(new TargetPosition{value = position});
            entity.AddData(new MoveDirection{value = Vector3.zero});
            entity.AddData(new MoveSpeed{value = 5});
            entity.AddData(new Rotation{value = transform.rotation});
            entity.AddData(new TransformView{value = transform});
            entity.AddData(new Team{value = team});
            entity.AddData(new AttackDistance{value = attackDistance});
            entity.AddData(new AttackTimeOut{value = attackTimeout});
            entity.AddData(new AttackActionTimeout{value = attackActionTimeout});
            entity.AddData(new AttackCurrentTimeout());
            entity.AddData(new Health{value = health});
            entity.AddData(new AttackDamage{value = damage});
            entity.AddData(new TargetEntity());
            entity.AddData(new DeathTimeout{value = deathTimeout});
            entity.AddData(new DeathCurrentTimeout());
            entity.AddData(new AnimatorView{value = animator});
            entity.AddData(new UnitAudioView{value = unitsAudioController});
            entity.AddData(new VfxView{value = vfxController});
            entity.AddData(new PreviousPosition{value = position});
            entity.AddData(new HealthCurrent{value = health});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
