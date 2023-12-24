using System;
using UnityEngine;
namespace MiscScripts
{
    public class OnDestroyObject : MonoBehaviour
    {
        private Action<OnDestroyObject> _killAction;
        [SerializeField] private int autoDestroyTimer;
        [SerializeField] private Type type;

        public void Init(Action<OnDestroyObject> killAction)
        {
            _killAction = killAction;
        }

        private void OnCollisionEnter(Collision collision)
        {
            _killAction(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _killAction(this);
            }
        }
    }
}

