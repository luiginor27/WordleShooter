using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public abstract class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform shootPoint;
    public float shootDelay;
    public BulletValues bulletValues;

    public Transform handPosition;
    public Transform aimPosition;

    public AudioClip shootSound;
    public AudioClip noAmmoSound;
    public AudioClip reloadSound;

    protected List<string> ammo;

    private float _shootTimer;

    private AudioSource _audioSource;


    private void Awake()
    {
        ammo = new List<string>();
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void Reload()
    {
        PlayReloadSound();
    }

    public void Shoot()
    {
        if (ammo.Count == 0)
        {
            PlayNoAmmoSound();
            return;
        }

        if (_shootTimer > 0)
        {
            _shootTimer -= Time.deltaTime;
        }
        else
        {
            _shootTimer = shootDelay;

            string bulletText = ammo[0];
            ammo.RemoveAt(0);

            GameObject bulletObject = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bulletObject.GetComponent<Bullet>().SetValues(bulletText, bulletValues.bulletSpeed, bulletValues.bulletDamage, bulletValues.isPiercing);

            PlayShootSound();
        }
    }

    public void AimOn()
    {
        transform.position = aimPosition.position;
    }

    public void AimOff()
    {
        transform.position = handPosition.position;
    }

    protected void PlayShootSound()
    {
        _audioSource.clip = shootSound;
        _audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.3f);
        _audioSource.Play();
    }

    protected void PlayNoAmmoSound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = noAmmoSound;
            _audioSource.Play();
        }
    }
    
    protected void PlayReloadSound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = reloadSound;
            _audioSource.Play();
        }
    }

    [Serializable]
    public struct BulletValues
    {
        public float bulletSpeed;
        public float bulletDamage;
        public bool isPiercing;
    }
}
