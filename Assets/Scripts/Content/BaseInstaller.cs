using EcsEngine.Components;
using EcsEngine.Components.Tags;
using EcsEngine.Components.Units;
using EcsEngine.Components.View;
using EcsEngine.Views;
using GameSystem;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class BaseInstaller : EntityInstaller
    {
        [SerializeField] private int team;
        [SerializeField] private float spawnTimeout;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int heath;
        [SerializeField] private float deathTimeout;
        [SerializeField] private BaseAudioController baseAudioController;
        [SerializeField] private VfxController vfxController;
        protected override void Install(Entity entity)
        {
            entity.AddData(new BaseTag());
            entity.AddData(new Team{value = team});
            entity.AddData(new Health{value = heath});
            entity.AddData(new HealthCurrent{value = heath});
            entity.AddData(new SpawnTimeout{value = spawnTimeout});
            entity.AddData(new SpawnTimeoutCurrent{value = spawnTimeout});
            entity.AddData(new UnitPrefab());
            entity.AddData(new SpawnPoint{value = spawnPoint});
            entity.AddData(new Position{value = transform.position});
            entity.AddData(new TransformView{value = transform});
            entity.AddData(new BaseAudioView{value = baseAudioController});
            entity.AddData(new VfxView{value = vfxController});
            entity.AddData(new DeathTimeout{value = deathTimeout});
            entity.AddData(new DeathCurrentTimeout{value = deathTimeout});
        }

        protected override void Dispose(Entity entity)
        {
            
        }
    }
}
