using EcsEngine.Components;
using EcsEngine.Components.Events;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems.View {
    internal sealed class AnimatorViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AnimatorView>> _filter;
        private readonly EcsPoolInject<MoveEvent> _moveEventPool;
        private readonly EcsPoolInject<StartAttackEvent> _startAttackEventPool;
        private readonly EcsPoolInject<DeathEvent> _deathEventPool;
        private readonly EcsPoolInject<DeadTag> _deadPool;
        private readonly EcsPoolInject<ReadyForAttackTag> _readyForAttackPool;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool;
        private static readonly int State = Animator.StringToHash("State");
        private static readonly int Attack = Animator.StringToHash("Attack");

        public void Run (IEcsSystems systems)
        {
            var animatorPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                if (_deathEventPool.Value.Has(entity))
                {
                    animatorPool.Get(entity).value.SetInteger(State, 3);
                    continue;
                }

                if (_deadPool.Value.Has(entity)) continue;
                

                if (!_readyForAttackPool.Value.Has(entity) && _targetEntityPool.Value.Get(entity).value >= 0)
                {
                    animatorPool.Get(entity).value.SetInteger(State, 1);
                    continue;
                }
                if (_startAttackEventPool.Value.Has(entity))
                {
                    animatorPool.Get(entity).value.SetTrigger(Attack);
                    continue;
                }
                animatorPool.Get(entity).value.SetInteger(State, 0);
            }
        }
    }
}