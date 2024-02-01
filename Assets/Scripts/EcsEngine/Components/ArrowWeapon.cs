using System;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine.Components 
{
    [Serializable]
    public struct ArrowWeapon
    {
        public Transform firePoint;
        public Entity arrowPrefab;
    }
}