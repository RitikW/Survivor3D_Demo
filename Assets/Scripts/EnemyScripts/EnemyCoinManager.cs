using MiscScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
namespace EnemyScripts
{
    public enum EnemyType
    {
        Walker,
        Ranged
    }

    public class EnemyCoinManager : MonoBehaviour
    {
        [SerializeField] private CanDie enemyPref;
        [SerializeField] private CanDie enemyPref2;
        [SerializeField] private OnDestroyObject Coins;
        [SerializeField] private TMP_Text coinsTxt;
        [SerializeField] private TMP_Text enemiesDefeatedTxt;
        [SerializeField] private int spawnAmount;
        [SerializeField] private float spawnTimer;
        private int coinsCollected = 0;
        private int enemiesDeafeated = 0;
        private ObjectPool<CanDie> _pool, _poolRanged;
        private ObjectPool<OnDestroyObject> _Coinpool;
        bool isEnded;

        public int CoinsCollected { get => coinsCollected; set => coinsCollected = value; }
        public int EnemiesDeafeated { get => enemiesDeafeated; set => enemiesDeafeated = value; }

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
            _pool = new ObjectPool<CanDie>(() =>
            {
                return Instantiate(enemyPref);
            }, auto =>
            {
                auto.gameObject.SetActive(true);
            }, auto =>
            {
                auto.gameObject.SetActive(false);
            }, auto =>
            {
                Destroy(auto.gameObject);
            }, false, 10, 100);
            InvokeRepeating(nameof(Spawn), 1, spawnTimer);

            _poolRanged = new ObjectPool<CanDie>(() =>
            {
                return Instantiate(enemyPref2);
            }, auto =>
            {
                auto.gameObject.SetActive(true);
            }, auto =>
            {
                auto.gameObject.SetActive(false);
            }, auto =>
            {
                Destroy(auto.gameObject);
            }, false, 10, 100);

            _Coinpool = new ObjectPool<OnDestroyObject>(() =>
            {
                return Instantiate(Coins);
            }, coin =>
            {
                coin.gameObject.SetActive(true);
            }, coin =>
            {
                coin.gameObject.SetActive(false);
            }, coin =>
            {
                Destroy(coin.gameObject);
            }, false, 10, 100);
        }

        private void Spawn()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                var auto = _pool.Get();
                auto.transform.position = transform.position + Random.insideUnitSphere * 50;
                auto.Init(KillAction);
                if (enemiesDeafeated > 15)
                {
                    var auto2 = _poolRanged.Get();
                    spawnTimer = 3;
                    auto2.transform.position = transform.position + Random.insideUnitSphere * 50;
                    auto2.Init(KillAction);
                }
                if(enemiesDeafeated>30)
                {
                    spawnAmount = 4;
                }
            }
        }

        private void KillAction(CanDie obj)
        {
            _pool.Release(obj);
            var coin = _Coinpool.Get();
            coin.transform.position = obj.transform.position;
            coin.Init(KillActionCoins);
            enemiesDeafeated += 1;
            enemiesDefeatedTxt.text = enemiesDeafeated.ToString();
        }

        private void KillActionCoins(OnDestroyObject obj)
        {
            _Coinpool.Release(obj);
            coinsCollected += 1;
            coinsTxt.text = coinsCollected.ToString();
        }
        public void OnGameOver()
        {
            CancelInvoke(nameof(Spawn));
            _Coinpool.Clear();
            _poolRanged.Clear();
            _pool.Clear();
        }

    }

}
