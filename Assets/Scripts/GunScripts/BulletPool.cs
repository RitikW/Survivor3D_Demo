using MiscScripts;
using UnityEngine;
using UnityEngine.Pool;
namespace GunScripts
{
    public class BulletPool : MonoBehaviour
    {
        public int bulletSpeed;
        [SerializeField] private OnDestroyObject bulletPrefab;
        private ObjectPool<OnDestroyObject> _bulletPool;

        private void OnEnable()
        {
            StaticHelper.OnGameOver += OnGameOver;
        }
        private void OnDisable()
        {
            StaticHelper.OnGameOver -= OnGameOver;
        }
        private void Start()
        {
            _bulletPool = new ObjectPool<OnDestroyObject>(() =>
            {
                return Instantiate(bulletPrefab);
            }, bullet =>
            {
                bullet.gameObject.SetActive(true);
            }, bullet =>
            {
                bullet.gameObject.SetActive(false);
            },bullet =>
            {
                Destroy(bullet.gameObject);
            },false,10, 100);
        }
        public void FireBullet(Vector3 position, Vector3 direction)
        {
            var bullet = _bulletPool.Get();
            bullet.transform.position = position;
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
            bullet.Init(KillAction);
        }
        private void KillAction(OnDestroyObject obj)
        {
            _bulletPool.Release(obj);
        }

        public void OnGameOver()
        {
            _bulletPool.Clear();
            _bulletPool.Dispose();
        }
    }

}
