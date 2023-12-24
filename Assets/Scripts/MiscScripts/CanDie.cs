using PlayerScripts;
using System;
using UnityEngine;
namespace MiscScripts
{
    public class CanDie : MonoBehaviour
    {
        private Action<CanDie> _killAction;
        private HealthPoints healthPoints;
        public PlayerType type;
        private void Start()
        {
            healthPoints = GetComponent<HealthPoints>();
        }
        public void Init(Action<CanDie> killAction)
        {
            _killAction = killAction;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (healthPoints.currentHP == 0)
            {
                if (type == PlayerType.enemy)
                {
                    _killAction(this);
                }
                else
                {
                    StaticHelper.OnGameOver?.Invoke();
                }
            }
        }
    }

}
