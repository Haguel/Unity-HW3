using StateMachine;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy.EnemyStateMachine.States
{
    public class ChaseState : AbstractState
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;

        private readonly int _chaseStateHash = Animator.StringToHash("Run");
        
        private Player.Player _player;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }

        public override void StartState()
        {
            base.StartState();
        }

        private void Update()
        {
            _animator.CrossFade(_chaseStateHash, 0f);
            _agent.SetDestination(_player.transform.position);
        }
        
        public override void ExitState()
        {
            base.ExitState();
        }

        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 10f);
        }

    }
}