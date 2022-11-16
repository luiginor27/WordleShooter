using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bullet : MonoBehaviour
{
    public float rotationSpeedMin;
    public float rotationSpeedMax;

    [HideInInspector]
    public float damage = 10f;

    private float _speed = 50f;
    private float _lifeTime = 1f;
    private bool _piercing = false;

    private float _lifeTimer;
    private float _rotationSpeed;

    private void Start()
    {
        _lifeTimer = _lifeTime;

        _rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * _rotationSpeed);

        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer < 0) Destroy(gameObject);
    }

    public void SetValues(string text, float speed, float damage, bool piercing)
    {
        GetComponent<TextMeshPro>().text = text;
        this._speed = speed;
        this.damage = damage;
        this._piercing = piercing;
    }

    public string GetText()
    {
        return GetComponent<TextMeshPro>().text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Ignore"))
        {
            if (!_piercing) Destroy(gameObject);
        }
    }
}
