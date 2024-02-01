using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine.Views
{
    public class UnitsAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource attackSound;
        [SerializeField] private AudioSource deathSound;


        public void OnAttack()
        {
            attackSound.Play();
        }

        public void OnDeath()
        {
            deathSound.Play();
        }
    }
}
