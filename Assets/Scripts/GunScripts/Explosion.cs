using UnityEngine;
namespace GunScripts
{
    public class Explosion : MonoBehaviour
    {
        public ParticleSystem particle;
        void OnEnable()
        {
            if (particle != null)
            {
                particle.Play();

                // destroy when particle effect ends
                Destroy(gameObject, particle.main.duration);
            }
        }
    }
}



