using EcsEngine.Components.Events;
using EcsEngine.Systems;
using EcsEngine.Systems.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace EcsEngine {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        IEcsSystems _systems;
        private EntityManager _entityManager;

        private void Awake()
        {
            _entityManager = new EntityManager();
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems
                .Add(new TargetSystem())
                .Add(new AttackDistanceCheckSystem())
                .Add(new AttackTimeoutSystem())
                .Add(new AttackRequestSystem())
                .Add(new AttackStartAttackSystem())
                .Add(new AttackActionSystem())
                .Add(new AttackFireRequestSystem())
                .Add(new RespawnArrowSystem())
                .Add(new SpawnArrowSystem())
                .Add(new MoveDirectionSetSystem())
                .Add(new MovementSystem())
                .Add(new ArrowHitSystem())
                .Add(new DeathRequestSystem())
                .Add(new DeathSystem())
                .Add(new DealDamageSystem())
                .Add(new UnitRespawnSystem())
                .Add(new UnitsSpawnTimeoutSystem())
                .Add(new UnitsSpawnSystem())
                .Add(new ArrowLifeTimeCheckSystem())

                //Views
                .Add(new RemoveArrowsSystem())
                .Add(new RemoveUnitsSystem())
                .Add(new TransformViewSystem())
                .Add(new AnimatorViewSystem())
                .Add(new UnitAudioSystem())
                .Add(new BaseAudioSystem())
                .Add(new VfxSystem())

#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))

                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                //.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.Events));
#endif
                //Clean Up
                .DelHere<AttackEvent>()
                .DelHere<MoveEvent>()
                .DelHere<StartAttackEvent>()
                .DelHere<DamageEvent>()
                .DelHere<SpawnUnitEvent>()
                .DelHere<DeathEvent>();
        }
        private void Start () {

            _entityManager.Initialize(_world);
            _systems.Inject(_entityManager);
            _systems.Init();
        }

        void Update () {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}