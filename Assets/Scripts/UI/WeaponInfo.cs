using System;
using System.Collections;
using SO;
using TMPro;
using UnityEngine;
using Weapon.RangeWeapon;
using Zenject;

namespace UI
{
    public class WeaponInfo : MonoBehaviour
    {
        
        [SerializeField] private CanvasGroup _weaponInfo;
        [SerializeField] private TMP_Text _ammoInfo;
        [SerializeField] private TMP_Text _reloadInfo;

        private Player.Player _player;
        private RangeWeapon _rangeWeapon;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }

        private void Awake()
        {
            _rangeWeapon = (RangeWeapon)_player.CurrentWeapon;
        }

        private void OnEnable()
        {
            _rangeWeapon.OnAmmoChanged += OnAmmoChanged;
            _rangeWeapon.OnReload += OnReload;
        }

        private void OnDisable()
        {
            _rangeWeapon.OnAmmoChanged -= OnAmmoChanged;
        }

        private void OnAmmoChanged(int maxAmmo, int totalAmmo)
        {
            _ammoInfo.text = $"{maxAmmo} / {totalAmmo}";
        }

        private void OnReload(float reloadTime)
        {
            StartCoroutine(ReloadInfo(reloadTime));
        }

        private IEnumerator ReloadInfo(float reloadTime)
        {
            _reloadInfo.gameObject.SetActive(true);
            yield return new WaitForSeconds(reloadTime);
            _reloadInfo.gameObject.SetActive(false);
        }
    }
}
