using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    public KeyCode shootKey;
    public KeyCode aimKey;
    public KeyCode reloadKey;
    public KeyCode changeKey;

    public List<Weapon> weapons;
    public Weapon currentWeapon;
    
    private int _weaponIndex = 0;

    private void Start()
    {
        currentWeapon = weapons[_weaponIndex];
    }

    private void OnEnable()
    {
        if(weapons.Count > 0) weapons[0].gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(reloadKey))
        {
            currentWeapon.Reload();
        }

        if (Input.GetKey(shootKey))
        {
            currentWeapon.Shoot();
        }

        if (Input.GetKeyDown(aimKey))
        {
            currentWeapon.AimOn();
        }

        if (Input.GetKeyUp(aimKey))
        {
            currentWeapon.AimOff();
        }

        if (Input.GetKeyDown(changeKey))
        {
            GetNextWeapon();
            currentWeapon.Reload();
        }
    }

    private void GetNextWeapon()
    {
        currentWeapon.gameObject.SetActive(false);
        int nextWeaponIndex = (_weaponIndex + 1) % weapons.Count;
        _weaponIndex = nextWeaponIndex;
        currentWeapon = weapons[nextWeaponIndex];
        currentWeapon.gameObject.SetActive(true);
    }
}
