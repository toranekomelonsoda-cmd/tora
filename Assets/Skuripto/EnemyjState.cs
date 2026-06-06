using UnityEngine;
using Core.Interface;
namespace TPSRoguelite.InGame.Enemy
{

    public class EnemyjState : MonoBehaviour,IDamageable
    {


        private const int MAX_HP = 100;
        public int CurrentHP { get; private set; }

        private void Awake()
        {
            CurrentHP = MAX_HP;
        }
        public void TakeDamage(int damageAmount)
        {
            //マイナスのダメージ（回復） を防ぐ
            if (damageAmount <= 0)
            {
                return;


            }

            CurrentHP -= damageAmount;
           Debug.Log($"敵に「damageAmount］のダメージ！残りHP:ICurrentHP");
        }
    }
}
