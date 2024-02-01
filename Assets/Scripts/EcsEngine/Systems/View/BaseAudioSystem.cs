using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems.View 
{
    internal sealed class BaseAudioSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<BaseTag, BaseAudioView>, Exc<InactiveTag>> _filter;
        private readonly EcsPoolInject<DamageEvent> _damageEventPool;
        private readonly EcsPoolInject<DeathEvent> _deathEventPool;
        private readonly EcsPoolInject<SpawnUnitEvent> _spawnEventPool;
        public void Run (IEcsSystems systems) 
        {
            foreach (var entity in _filter.Value)
            {
                if (_deathEventPool.Value.Has(entity))
                {
                    _filter.Pools.Inc2.Get(entity).value.WhenDestroy();
                    continue;
                }
                if (_damageEventPool.Value.Has(entity))
                {
                    _filter.Pools.Inc2.Get(entity).value.OnDamage();
                }

                if (_spawnEventPool.Value.Has(entity))
                {
                    _filter.Pools.Inc2.Get(entity).value.OnRecruit();
                }
            }
        }
    }
}