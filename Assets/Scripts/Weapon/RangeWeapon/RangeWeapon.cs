using System.Collections;
using SO;
using UnityEngine;
using UnityEngine.Events;

namespace Weapon.RangeWeapon
{
    public abstract class RangeWeapon : Weapon
    {
        [SerializeField] private RangeWeaponData _data;
        [SerializeField] private Camera _camera;
        
        private readonly Vector3 _rayPosition = new Vector3(0.5f, 0.5f, 0f);
        
        private float _damage;
        private float _range;
        private int _maxAmmo;
        private int _totalAmmo;
        private float _reloadTime;
        
        private bool _isReloading;
        private int _currentAmmo;
        
        public event UnityAction<int, int> OnAmmoChanged;
        public event UnityAction<float> OnReload;

        private void Awake()
        {
            _damage = _data.Damage;
            _range = _data.Range;
            _maxAmmo = _data.MaxAmmo;
            _totalAmmo = _data.TotalAmmo;
            _reloadTime = _data.ReloadTime;
        }

        private void Start()
        {
            _currentAmmo = _maxAmmo;
            OnAmmoChanged?.Invoke(_currentAmmo, _totalAmmo);
        }

        protected override void Attack()
        {
            if (PlayerInput.AttackInput)
            {
                Shoot();
            }
            else if (PlayerInput.ReloadInput)
            {
                Reload();
            }
        }

        private void Shoot()
        {
            if (_isReloading || _currentAmmo <= 0) return;
            
            ShootSound();
            var ray = _camera.ViewportPointToRay(_rayPosition);

            if (Physics.Raycast(ray, out var hit, _range))
            {
                if (hit.transform.TryGetComponent(out Enemy.Enemy enemy))
                {
                    print(enemy.name);
                    enemy.TakeDamage(_damage);
                }
            }

            _currentAmmo--;
            OnAmmoChanged?.Invoke(_currentAmmo, _totalAmmo);
        }

        private void Reload()
        {
            if (_currentAmmo < _maxAmmo && _totalAmmo > 0 && !_isReloading)
            {
                StartCoroutine(ReloadDelay());
            }
        }
        
        private IEnumerator ReloadDelay()
        {
            _isReloading = true;
            OnReload?.Invoke(_reloadTime);
            yield return new WaitForSeconds(_reloadTime);
            
            var ammoToReload = _maxAmmo - _currentAmmo;
            var minAmmoToReload = Mathf.Min(ammoToReload, _totalAmmo);
            
            _currentAmmo += minAmmoToReload;
            _totalAmmo -= minAmmoToReload;
            
            OnAmmoChanged?.Invoke(_currentAmmo, _totalAmmo);
            
            _isReloading = false;
        }

        private void ShootSound()
        {
            AttackSound.pitch = Random.Range(0.7f, 1.5f);
            AttackSound.Play();
        }
        
        private void OnDrawGizmos()
        {
            Debug.DrawRay(_rayPosition, _rayPosition * _range, Color.red);
        }
    }
}