using MiscScripts;
using UnityEngine;
namespace GunScripts
{
    public class BulletDamage : MonoBehaviour
    {
        public GameObject explode;
        public int damage = 20;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
            {
                TakeDamage(collision, damage);
            }
            foreach (ContactPoint contact in collision.contacts)
            {
                Instantiate(explode, contact.point, Quaternion.identity);
            }
        }
        public void TakeDamage(Collision collision, int damage)
        {
            //Applies Damage to user we hit;
            HealthPoints hp = collision.gameObject.GetComponent<HealthPoints>();

            if (hp != null)
            {
                hp.GotHit(damage);
            }
        }
    }
}