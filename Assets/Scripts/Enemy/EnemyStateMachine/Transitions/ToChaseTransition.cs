using System;
using StateMachine;
using UnityEngine;
using Zenject;

namespace Enemy.EnemyStateMachine.Transitions
{
    public class ToChaseTransition : AbstractTransition
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private float _detectedRadius;

        private Player.Player _player;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }
        
        private void Update()
        {
            if (Vector3.Distance(_enemy.transform.position, _player.transform.position) <= _detectedRadius)
            {
                ShouldTransition = true;
            }
        }
    }
}