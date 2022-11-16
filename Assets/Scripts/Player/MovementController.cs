using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float maxSpeed = 5f;

    private float _horizontalMovement;
    private float _verticalMovement;

    private float _currentSpeed;

    private Camera _camera;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        //enabled = false;
    }

    void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void GetInputs()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
    }

    private void MoveCharacter()
    {
        // Movimiento X-Z del input
        Vector3 movement = new Vector3(_horizontalMovement, 0f, _verticalMovement);

        // Obtenemos el desplazamiento del Input
        _currentSpeed = (movement.magnitude > 1 ? 1 : movement.magnitude);

        // Normalizamos y lo hacemos proporcional a la velocidad por segundo
        movement = movement.normalized * maxSpeed * Time.deltaTime;

        // Rotamos el vector para que se ajuste a la rotación de la cámara
        movement = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * movement;

        // Desplazamos el personaje
        _rigidbody.MovePosition(transform.position + (movement * _currentSpeed));
    }
}
