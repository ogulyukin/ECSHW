using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems 
{
    internal sealed class ArrowHitSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Position, AttackDamage, WeaponTag>, Exc<InactiveTag>> _filter;
        private readonly EcsFilterInject<Inc<Position, Team>, Exc<InactiveTag, DeadTag, WeaponTag>> _targetFilter; 
        private readonly EcsPoolInject<Team> _teamPool;
        private readonly EcsPoolInject<DamageEvent> _damageEventPool;
        private readonly EcsPoolInject<ArrowRemoveRequest> _arrowRemoveRequestsPool;
        public void Run (IEcsSystems systems) 
        {
            var arrowPositionPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                var arrowTeam = _teamPool.Value.Get(entity);
                var arrowPosition = arrowPositionPool.Get(entity).value;
                arrowPosition.y = 0;
                foreach (var target in _targetFilter.Value)
                {
                    if(_targetFilter.Pools.Inc2.Get(target).value == arrowTeam.value)
                        continue;
                    if (Vector3.Distance(_targetFilter.Pools.Inc1.Get(target).value, arrowPosition) < 0.3f)
                    {
                        if (!_damageEventPool.Value.Has(target))
                        {
                            _damageEventPool.Value.Add(target).value = _filter.Pools.Inc2.Get(entity).value;
                        }
                        else
                        {
                            _damageEventPool.Value.Get(target).value += _filter.Pools.Inc2.Get(entity).value;
                        }
                        if (!_arrowRemoveRequestsPool.Value.Has(entity))
                        {
                            _arrowRemoveRequestsPool.Value.Add(entity);
                        }
                    }
                }
            }
        }
    }
}