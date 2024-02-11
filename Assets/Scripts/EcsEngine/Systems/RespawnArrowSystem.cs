using System;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace EcsEngine.Systems 
{
    internal sealed class RespawnArrowSystem : IEcsRunSystem 
    {
        private readonly EcsFilterInject<Inc<FireRequest, ArrowWeapon>, Exc<InactiveTag, DeadTag>> _filter;
        private readonly EcsFilterInject<Inc<WeaponTag, TransformView, Position, InactiveTag, LifeTime, LifeTimeCurrent>> _arrowsFilter;
        private readonly EcsFilterInject<Inc<ArrowPoolContainerTag, Container>> _poolFilter;
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
                if(_arrowsFilter.Value.GetEntitiesCount() == 0)
                    return;
                var arrow = -1;
                foreach (var storedArrow in _arrowsFilter.Value)
                {
                    arrow = storedArrow;
                    break;
                }
                var arrowConfig = weaponPool.Get(entity);
                var position = arrowConfig.firePoint.position;
                _arrowsFilter.Pools.Inc2.Get(arrow).value.SetParent(_poolFilter.Pools.Inc2.Get(poolEntity).value);
                _arrowsFilter.Pools.Inc3.Get(arrow).value = position;
                _targetPositionPool.Value.Get(arrow).value = _targetPositionPool.Value.Get(entity).value;
                _teamPool.Value.Get(arrow).value = _teamPool.Value.Get(entity).value;
                _arrowsFilter.Pools.Inc6.Get(arrow).value = _arrowsFilter.Pools.Inc5.Get(arrow).value;
                _arrowsFilter.Pools.Inc4.Del(arrow);
                requestPool.Del(entity);
            }
        }
    }
}