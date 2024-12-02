using StateMachine;
using UnityEngine;
using Zenject;

namespace Enemy.EnemyStateMachine.Transitions
{
    public class ChaseToTransition : AbstractTransition
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private float _chaseRadius;
        
        private Player.Player _player;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
        private void Update()
        {
            if (Vector3.Distance(_player.transform.position, _enemy.transform.position) >= _chaseRadius)
            {
                ShouldTransition = true;
            }
        }
    }
}