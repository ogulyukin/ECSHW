using UnityEngine;

namespace EcsEngine.Views
{
    public class VfxController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem damage;

        public void OnDamage()
        {
            damage.Play();
        }

    }
}
