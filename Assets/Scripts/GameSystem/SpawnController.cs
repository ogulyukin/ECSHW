using System.Collections.Generic;
using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Components.Units;
using Leopotam.EcsLite.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameSystem
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private Entity entity;
        [SerializeField] private List<Entity> prefabsList;

        [Button]
        private void Spawn()
        {
            if (!entity.HasData<UnitSpawnRequest>())
            {
                entity.GetData<UnitPrefab>().value = prefabsList[Random.Range(0, prefabsList.Count)];
                entity.AddData(new UnitSpawnRequest());
                Debug.Log($"Spawn! {entity.GetData<Team>()}");    
            }
        }
    }
}
