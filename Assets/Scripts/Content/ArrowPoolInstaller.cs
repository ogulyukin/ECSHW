using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.View;
using Leopotam.EcsLite.Entities;
using UnityEngine;


namespace Content
{
    public class ArrowPoolInstaller : EntityInstaller
    {
        [SerializeField] private Transform containerForInactive;
        [SerializeField] private Transform container;
        protected override void Install(Entity entity)
        {
            entity.AddData(new ArrowPoolContainerTag());
            entity.AddData(new ContainerForInactive{value = containerForInactive});
            entity.AddData(new Container{value = container});
            entity.AddData(new Position{value = containerForInactive.transform.position});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
