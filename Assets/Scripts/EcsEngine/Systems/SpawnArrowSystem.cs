using System;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine.Systems 
{
    internal sealed class SpawnArrowSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FireRequest, ArrowWeapon>, Exc<InactiveTag, DeadTag>> _filter;
        private readonly EcsFilterInject<Inc<ArrowPoolContainerTag, Container>> _poolFilter;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        private readonly EcsPoolInject<TargetPosition> _targetPositionPool;
        private readonly EcsPoolInject<Team> _teamPool;

        public void Run (IEcsSystems systems) 
        {
            var weaponPool = _filter.Pools.Inc2;
            var requestPool = _filter.Pools.Inc1;
            var poolEntity = -1;
            foreach (var entity in _poolFilter.Value)
            {
                poolEntity = entity;
            }

            if (poolEntity == -1)
            {
                throw new Exception("No arrow pool container found!");
            }
            foreach (var entity in _filter.Value)
            {
                var arrow = weaponPool.Get(entity);
                var position = arrow.firePoint.position;
                var newArrow =_entityManager.Value.Create(arrow.arrowPrefab, position,
                    Quaternion.Euler(Vector3.left), _poolFilter.Pools.Inc2.Get(poolEntity).value);
                _targetPositionPool.Value.Get(newArrow.Id).value = _targetPositionPool.Value.Get(entity).value;
                _teamPool.Value.Get(newArrow.Id).value = _teamPool.Value.Get(entity).value;
                requestPool.Del(entity);
            }
        }
    }
}