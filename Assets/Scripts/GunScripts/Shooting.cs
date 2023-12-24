using EnemyScripts;
using UnityEngine;
namespace GunScripts
{
    public class Shooting : MonoBehaviour
    {
        public float attackRange;
        private IClosestEnemy closestEnemy;
        private float inRange;
        public BulletPool gun;
        public Transform launchPosition;
        private bool isEnded;

        private void OnEnable()
        {
            StaticHelper.OnGameOver += OnGameOver;
        }
        private void OnDisable()
        {
            StaticHelper.OnGameOver -= OnGameOver;
        }
        private void FixedUpdate()
        {
            if(!isEnded)
            {
                AutoFindClosestEnemy();
                ShootInRange();
            }
        }
        private void ShootInRange()
        {
            if (closestEnemy != null)
            {
                inRange = Vector3.Distance(transform.position, closestEnemy.transform.position);
                if (attackRange >= inRange && closestEnemy.transform.gameObject.activeInHierarchy)
                {
                    Quaternion rotation = Quaternion.LookRotation(closestEnemy.transform.localPosition - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 25f);
                    if (!IsInvoking("FireBullet"))
                    {
                        InvokeRepeating("FireBullet", 0f, .4f);
                    }
                }
                else
                {
                    CancelInvoke("FireBullet");
                }
            }
        }
        private void AutoFindClosestEnemy()
        {
            int maxCol = 50;
            Collider[] hitCol = new Collider[maxCol];
            int numCol = Physics.OverlapSphereNonAlloc(transform.position, attackRange, hitCol);
            float closestDistance = Mathf.Infinity;
            for (int i = 0; i < numCol; i++)
            {
                IClosestEnemy closest;
                hitCol[i].TryGetComponent(out closest);
                if (closest != null)
                {
                    float distance = Vector3.Distance(transform.position, hitCol[i].transform.position);
                    if (distance < closestDistance)
                    {
                        if (closestEnemy != null)
                        {
                            closestEnemy.Untarget();
                        }
                        closestEnemy = closest;
                        closestDistance = distance;
                        closestEnemy.Target();
                    }
                }
            }
        }
        void FireBullet()
        {
            if(!isEnded)
            {
                gun.FireBullet(launchPosition.position, transform.forward);
            }     
        }

        public void OnGameOver()
        {
            isEnded = true;
        }
    }

}
