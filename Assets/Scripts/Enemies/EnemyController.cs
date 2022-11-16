using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        APPROACHING, ATTACKING, COOLDOWN
    }

    public float damage = 10f;
    public float speed = 5f;
    public float maxHealth = 100;

    public float attackingDistance = 2f;
    public float attackCooldown = 1f;

    public AudioClip hitSound;
    public AudioClip spawnSound;

    private EnemyState _state;

    private Transform _player;
    private float _currentHealth;

    private bool _attacking;
    private float _cooldownTimer;

    private float _animatorSpeed;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private AudioSource _audioSource;

    void Start()
    {
        _state = EnemyState.APPROACHING;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (_animatorSpeed == 0)
        {
            if (_audioSource == null)
                _audioSource = GetComponent<AudioSource>();

            PlaySpawnSound();
            RestoreHealth();
        }
        else
        {
            _animator.speed = _animatorSpeed;
        }
    }

    private void OnDisable()
    {
        _animatorSpeed = _animator.speed;
        _animator.speed = 0;
    }

    private void Update()
    {
        switch (_state)
        {
            // Get close to the player
            case EnemyState.APPROACHING:

                if (Vector3.Distance(transform.position, _player.position) < attackingDistance)
                {
                    _state = EnemyState.ATTACKING;
                    _animator.SetBool("Walking", false);
                }
                break;

            // Trigger attack
            case EnemyState.ATTACKING:

                if (!_attacking)
                {
                    _attacking = true;
                    _cooldownTimer = attackCooldown;
                    _animator.SetTrigger("Attack");
                }
                break;

            // Wait until next attack
            case EnemyState.COOLDOWN:

                _cooldownTimer -= Time.deltaTime;
                if (_cooldownTimer < 0)
                {
                    _state = EnemyState.ATTACKING;
                }
                if (Vector3.Distance(transform.position, _player.position) > attackingDistance)
                {
                    _state = EnemyState.APPROACHING;
                    _animator.SetBool("Walking", true);
                }
                break;
        }
    }

    void FixedUpdate()
    {
        switch (_state)
        {
            case EnemyState.APPROACHING:

                Vector3 direction = _player.position - transform.position;
                _rigidbody.rotation = Quaternion.LookRotation(direction);

                Vector3 movement = transform.forward;
                movement = movement.normalized * speed * Time.deltaTime;

                movement = Quaternion.Euler(0, transform.rotation.y, 0) * movement;

                _rigidbody.MovePosition(transform.position + (movement));
                break;

            case EnemyState.ATTACKING:
                break;
            case EnemyState.COOLDOWN:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _currentHealth -= other.gameObject.GetComponent<Bullet>().damage;

            if (_currentHealth <= 0) gameObject.SetActive(false);
        }
    }

    // Triggered when the attack animation hits
    public void MakeDamage()
    {
        if (Vector3.Distance(transform.position, _player.position) <= attackingDistance)
        {
            _player.gameObject.GetComponent<PlayerHealth>().Hit(damage);
            PlayHitSound();
        }
    }

    // Triggered when the attack animation ends
    public void StartCooldown()
    {
        _state = EnemyState.COOLDOWN;
        _attacking = false;
    }

    private void RestoreHealth()
    {
        _currentHealth = maxHealth;
    }

    public void PlayHitSound()
    {
        _audioSource.clip = hitSound;
        _audioSource.Play();
    }

    public void PlaySpawnSound()
    {
        _audioSource.clip = spawnSound;
        _audioSource.Play();
    }
}
