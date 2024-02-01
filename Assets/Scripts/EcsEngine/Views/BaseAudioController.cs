using UnityEngine;

namespace EcsEngine.Views
{
    public class BaseAudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource recruitSound;
        [SerializeField] private AudioSource damageSound;
        [SerializeField] private AudioSource destroySound;

        public void OnRecruit()
        {
            recruitSound.Play();
        }
        
        public void OnDamage()
        {
            damageSound.Play();
        }
        
        public void WhenDestroy()
        {
            destroySound.Play();
        }
    }
}
