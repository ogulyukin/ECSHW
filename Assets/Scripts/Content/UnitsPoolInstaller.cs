using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class UnitsPoolInstaller : EntityInstaller
    {
        [SerializeField] private Transform container;
        [SerializeField] private Transform containerForInactive;
        protected override void Install(Entity entity)
        {
            entity.AddData(new UnitPoolContainerTag());
            entity.AddData(new Container{value = container});
            entity.AddData(new ContainerForInactive{value = containerForInactive});
            entity.AddData(new Position{value = container.transform.position});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
