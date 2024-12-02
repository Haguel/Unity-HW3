using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Weapon.Weapon _startWeapon;

        private Weapon.Weapon _currentWeapon;

        public Weapon.Weapon CurrentWeapon => _currentWeapon;
        
        private void Awake()
        {
            _currentWeapon = _startWeapon;
        }
    }
}
