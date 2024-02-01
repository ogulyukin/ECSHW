using System;
using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Components.Units;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems 
{
    internal sealed class UnitRespawnSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<UnitSpawnRequest, SpawnPoint, UnitPrefab, Team, SpawnTimeout, SpawnTimeoutCurrent>> _filter;
        private readonly EcsFilterInject<Inc<UnitTag, InactiveTag>, Exc<DeadTag>> _inactiveUnitsFilter;
        private readonly EcsFilterInject<Inc<UnitPoolContainerTag, Container>> _poolFilter;
        private readonly EcsPoolInject<Team> _teamPool;
        private readonly EcsPoolInject<TransformView> _transformViewPool;
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<HealthCurrent> _healthCurrentPool;
        private readonly EcsPoolInject<AttackingTag> _attackingTagPool;
        private readonly EcsPoolInject<ReadyForAttackTag> _readyForAttackTagPool;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<SpawnUnitEvent> _spawnUnitEventPool;
        public void Run (IEcsSystems systems) 
        {
            var poolEntity = -1;
            foreach (var entity in _poolFilter.Value)
            {
                poolEntity = entity;
            }

            if (poolEntity == -1)
            {
                throw new Exception("No unit pool container found!");
            }
            
            foreach (var entity in _filter.Value)
            {
                var team = _filter.Pools.Inc4.Get(entity);
                var prefabName = _filter.Pools.Inc3.Get(entity).value.name;
                foreach (var unit in _inactiveUnitsFilter.Value)
                {
                    if (_teamPool.Value.Get(unit).value == team.value && _transformViewPool.Value.Get(unit).value.gameObject.name.Contains(prefabName))
                    {
                        _healthCurrentPool.Value.Get(unit).value = _healthPool.Value.Get(unit).value;
                        if (_attackingTagPool.Value.Has(unit))
                        {
                            _attackingTagPool.Value.Del(unit);
                        }
                        if (_readyForAttackTagPool.Value.Has(unit))
                        {
                            _readyForAttackTagPool.Value.Del(unit);
                        }

                        _targetEntityPool.Value.Get(unit).value = -1;
                        _transformViewPool.Value.Get(unit).value.SetParent(_poolFilter.Pools.Inc2.Get(poolEntity).value);
                        _positionPool.Value.Get(unit).value = _filter.Pools.Inc2.Get(entity).value.position;
                        _inactiveUnitsFilter.Pools.Inc2.Del(unit);
                        _filter.Pools.Inc1.Del(entity);
                        _spawnUnitEventPool.Value.Add(entity);
                        break;
                    }
                }
            }
        }
    }
}