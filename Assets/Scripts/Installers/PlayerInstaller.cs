using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player.Player _player;
        [SerializeField] private Transform _spawnPoint;
        
        public override void InstallBindings()
        {
            var player = Container
                .InstantiatePrefabForComponent<Player.Player>(_player, _spawnPoint.position, Quaternion.identity, null);

            Container
                .Bind<Player.Player>()
                .FromInstance(player)
                .AsSingle()
                .NonLazy();
        }
    }
}