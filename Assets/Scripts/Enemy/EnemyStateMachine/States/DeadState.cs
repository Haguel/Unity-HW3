using StateMachine;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using System.Collections;

namespace Enemy.EnemyStateMachine.States
{
    public class DeadState : AbstractState
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _initialSinkSpeed = 0.5f;
        [SerializeField] private float _finalSinkSpeed = 2.5f;
        [SerializeField] private Transform _model;
        [SerializeField] private NavMeshAgent _agent;
        private readonly int _deadStateHash = Animator.StringToHash("Die");
        private bool _isSinking = false;
        private bool _isFinalSinking = false;

        public override void StartState()
        {
            print("Dead state entered");
            base.StartState();

            _agent.SetDestination(transform.position);
            _animator.CrossFade(_deadStateHash, 0f);

            StartCoroutine(SinkAfterDelay(1.0f));
        }

        private IEnumerator SinkAfterDelay(float delay)
        {
            print("Waiting for " + delay + " seconds before initial sinking");
            yield return new WaitForSeconds(delay);

            // Start initial sinking
            _isSinking = true;
            print("Model is now sinking a little");

            yield return new WaitForSeconds(5.0f);

            // Start final sinking
            _isSinking = false;
            _isFinalSinking = true;
            print("Model is now sinking underground");
        }

        private void Update()
        {
            if (_isSinking && _model != null)
            {
                print("Sinking model a little");
                _model.Translate(Vector3.down * _initialSinkSpeed * Time.deltaTime, Space.World);
            }
            else if (_isFinalSinking && _model != null)
            {
                print("Sinking model underground");
                _model.Translate(Vector3.down * _finalSinkSpeed * Time.deltaTime, Space.World);
            }
        }

        public override void ExitState()
        {
            print("Exiting DeadState");
            base.ExitState();
            _isSinking = false;
            _isFinalSinking = false;
        }
    }
}