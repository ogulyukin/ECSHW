using EcsEngine.Components;
using EcsEngine.Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems 
{
    internal sealed class TargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Team, Position, TargetPosition>, Exc<AttackingTag, InactiveTag, DeadTag, WeaponTag>> _filter;
        private readonly EcsFilterInject<Inc<Team, Position>, Exc<InactiveTag, DeadTag, WeaponTag>> _targetFilter;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool;
        public void Run (IEcsSystems systems) 
        {
            var teamPool = _filter.Pools.Inc1;
            var positionPool = _filter.Pools.Inc2;
            var targetPositionPool = _filter.Pools.Inc3;
            
            var currentDistance = 0f;
            
            foreach (var entity in _filter.Value)
            {
                var targetFound = false;
                foreach (var enemy in _targetFilter.Value)
                {
                    if(teamPool.Get(enemy).value == teamPool.Get(entity).value) continue;
                    if (!targetFound)
                    {
                        targetPositionPool.Get(entity).value = positionPool.Get(enemy).value;
                        currentDistance =
                            Vector3.Distance(positionPool.Get(enemy).value, positionPool.Get(entity).value);
                        AddEnemyEntityAsTarget(entity, enemy);
                        targetFound = true;
                    }
                    else
                    {
                        var currentEnemy = positionPool.Get(enemy).value;
                        var entityPosition = positionPool.Get(entity).value;
                        var distance = Vector3.Distance(currentEnemy, entityPosition);
                        if (!(distance < currentDistance)) continue;
                        targetPositionPool.Get(entity).value = currentEnemy;
                        AddEnemyEntityAsTarget(entity, enemy);
                        currentDistance = distance;
                    }
                }
                if (!targetFound)
                {
                    targetPositionPool.Get(entity).value = positionPool.Get(entity).value;
                    _targetEntityPool.Value.Get(entity).value = -1;
                }
            }
        }

        private void AddEnemyEntityAsTarget(int entity, int enemy)
        {
            if (_targetEntityPool.Value.Has(entity))
            {
                _targetEntityPool.Value.Get(entity).value = enemy;
            }
        }
    }
}