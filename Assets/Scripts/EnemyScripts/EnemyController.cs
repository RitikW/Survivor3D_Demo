using GunScripts;
using MiscScripts;
using UnityEngine;
using UnityEngine.AI;
namespace EnemyScripts
{
    public enum EnemyState
    {
        CHASE,
        ATTACK
    }

    public class EnemyController : MonoBehaviour, IClosestEnemy
    {
        private NavMeshAgent navagent;
        private Transform target1;
        private EnemyState enem_state;
        private HealthPoints playerHP;
        private float current_chase_dist;
        private float attack_timer =1;

        [Header("Enemy Stats")]
        [SerializeField] private EnemyType enemyType; 
        [SerializeField] private float run_speed;
        [SerializeField] private float chase_dist;
        [SerializeField] private float attack_dist;
        [SerializeField] private float chase_after_attack;
        [SerializeField] private float attack_pause;
        [SerializeField] private int enemyDamage;
        [SerializeField] private BulletPool _pool;
        [SerializeField] private Transform lauchPos;

        // Start is called before the first frame update
        void Awake()
        {
            navagent = GetComponent<NavMeshAgent>();

            target1 = GameObject.FindWithTag("Player").transform;
            playerHP = target1.GetComponent<HealthPoints>();
        }

        void Start()
        {
            enem_state = EnemyState.CHASE;

            if(enemyType == EnemyType.Walker)
            {
                _pool = null;
                lauchPos = null;
            }

            attack_timer = attack_pause;
            current_chase_dist = chase_dist;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (enem_state == EnemyState.CHASE)
            {
                Chase();
            }
            if (enem_state == EnemyState.ATTACK)
            {
                Attack();
            }
        }

        void Chase()
        {
            navagent.isStopped = false;
            navagent.speed = run_speed;
            navagent.SetDestination(target1.position);
            if (Vector3.Distance(transform.position, target1.position) <= attack_dist)
            {
                enem_state = EnemyState.ATTACK;

                if (chase_dist != current_chase_dist)
                {
                    chase_dist = current_chase_dist;
                }
            }
            else if (Vector3.Distance(transform.position, target1.position) > chase_dist)
            {
                enem_state = EnemyState.CHASE;

                if (chase_dist != current_chase_dist)
                {
                    chase_dist = current_chase_dist;
                }
            }
        }
        void Attack()
        {
            navagent.velocity = Vector3.zero;
            navagent.isStopped = true;
            attack_timer += Time.deltaTime;
            if (attack_timer > attack_pause)
            {
                ExecuteAttack();
                attack_timer = 0f;
            }

            if (Vector3.Distance(transform.position, target1.position) > attack_dist + chase_after_attack)
            {
                enem_state = EnemyState.CHASE;
                attack_timer = 1;
            }
        }

        private void ExecuteAttack()
        {
            if(enemyType == EnemyType.Walker && playerHP != null)
            {
                playerHP.GotHit(enemyDamage);
                if(playerHP.currentHP==0)
                {
                    StaticHelper.OnGameOver?.Invoke();
                }
            }
            else
            {
                _pool.FireBullet(lauchPos.position, transform.forward);
            }

        }

        public void Target()
        {

        }

        public void Untarget()
        {

        }
    }
}

