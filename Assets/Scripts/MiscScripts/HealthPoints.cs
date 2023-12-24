using UnityEngine;
using UnityEngine.UI;
namespace MiscScripts
{
    public enum PlayerType { player, enemy };
    public class HealthPoints : MonoBehaviour
    {
        public int maxHp = 100;
        public Slider hpSlider;
        public int currentHP;

        void OnEnable()
        {
            hpSlider.minValue = 0;
            hpSlider.maxValue = maxHp;
            currentHP = maxHp;
            hpSlider.value = currentHP;
        }

        public void GotHit(int damage)
        {
            if (currentHP > 0)
            {
                currentHP = currentHP - damage;
                hpSlider.value = currentHP;
                if (currentHP < 0)
                {
                    currentHP = 0;
                }
            }
        }
    }
}

