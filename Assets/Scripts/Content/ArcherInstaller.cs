using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using EcsEngine.Views;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class ArcherInstaller : EntityInstaller
    {
        [SerializeField] private int team;
        [SerializeField] private float attackDistance;
        [SerializeField] private float attackTimeout;
        [SerializeField] private float fireTimeout;
        [SerializeField] private Entity arrowPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private int health;
        [SerializeField] private float deathTimeout;
        [SerializeField] private Animator animator;
        [SerializeField] private UnitsAudioController unitsAudioController;
        [SerializeField] private VfxController vfxController;
        protected override void Install(Entity entity)
        {
            var transformPosition = transform.position;
            entity.AddData(new UnitTag());
            entity.AddData(new Position{value = transformPosition});
            entity.AddData(new TargetPosition{value = transformPosition});
            entity.AddData(new MoveDirection{value = Vector3.zero});
            entity.AddData(new MoveSpeed{value = 5});
            entity.AddData(new Rotation{value = transform.rotation});
            entity.AddData(new TransformView{value = transform});
            entity.AddData(new Team{value = team});
            entity.AddData(new AttackDistance{value = attackDistance});
            entity.AddData(new AttackTimeOut{value = attackTimeout});
            entity.AddData(new AttackFireTimeout{value = fireTimeout});
            entity.AddData(new AttackCurrentTimeout());
            entity.AddData(new Health{value = health});
            entity.AddData(new HealthCurrent{value = health});
            entity.AddData(new TargetEntity{value = -1});
            entity.AddData(new DeathTimeout{value = deathTimeout});
            entity.AddData(new DeathCurrentTimeout());
            entity.AddData(new AnimatorView{value = animator});
            entity.AddData(new UnitAudioView{value = unitsAudioController});
            entity.AddData(new VfxView{value = vfxController});
            entity.AddData(new PreviousPosition{value = transformPosition});
            entity.AddData(new ArrowWeapon
            {
                arrowPrefab = arrowPrefab,
                firePoint = firePoint,
            });
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
