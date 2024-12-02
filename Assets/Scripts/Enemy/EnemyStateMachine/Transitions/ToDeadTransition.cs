using StateMachine;
using UnityEngine;
using Zenject;

namespace Enemy.EnemyStateMachine.Transitions
{
    public class ToDeadTransition : AbstractTransition
    {
        [SerializeField] private Enemy _enemy;
        private bool _hasTransitioned = false;

        private void Update()
        {
            if (!_hasTransitioned && _enemy.CurrentHealth <= 0)
            {
                ShouldTransition = true;
                _hasTransitioned = true;
            }
        }
    }
}