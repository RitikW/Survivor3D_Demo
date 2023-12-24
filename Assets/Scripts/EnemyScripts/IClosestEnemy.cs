using UnityEngine;
namespace EnemyScripts
{
    public interface IClosestEnemy
    {
        void Target();
        void Untarget();
        Transform transform { get; }
    }
}

