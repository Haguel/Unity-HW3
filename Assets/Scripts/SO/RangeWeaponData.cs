using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "Weapons/RangeWeaponData", fileName = "NewRangeWeaponData")]
    public class RangeWeaponData : ScriptableObject
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _range;
        [SerializeField] private int _maxAmmo;
        [SerializeField] private int _totalAmmo;
        [SerializeField] private float _reloadTime;
        
        public float Damage => _damage;
        public float Range => _range;
        public int MaxAmmo => _maxAmmo;
        public int TotalAmmo => _totalAmmo;
        public float ReloadTime => _reloadTime;
    }
}
