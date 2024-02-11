using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems.View 
{
    internal sealed class RemoveArrowsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ArrowRemoveRequest,Position, TransformView>, Exc<InactiveTag>> _filter;
        private readonly EcsFilterInject<Inc<ArrowPoolContainerTag, Position, ContainerForInactive>> _poolContainer;
        private readonly EcsPoolInject<InactiveTag> _inactivePool;
        public void Run (IEcsSystems systems)
        {
            var container = -1;
            foreach (var containerEntity in _poolContainer.Value)
            {
                container = containerEntity;
            }
            foreach (var entity in _filter.Value)
            {
                _inactivePool.Value.Add(entity);
                _filter.Pools.Inc3.Get(entity).value.SetParent(_poolContainer.Pools.Inc3.Get(container).value);
                _filter.Pools.Inc2.Get(entity).value = _poolContainer.Pools.Inc2.Get(container).value;
                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}