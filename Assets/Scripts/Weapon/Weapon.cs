using Player;
using UnityEngine;

namespace Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected PlayerInput PlayerInput;
        [SerializeField] protected AudioSource AttackSound;

        protected abstract void Attack();
    }
}
