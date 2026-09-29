
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public float Speed = 2.0f;
    public float JumpForce = 150.0f;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movimiento horizontal (A/D o Flechas)
        Horizontal = Input.GetAxisRaw("Horizontal");

        // Orientación del personaje (Flip en X)
        if (Horizontal < 0.0f) 
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) 
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Activación de la animación 'running' en el Animator
        Animator.SetBool("running", Horizontal != 0.0f);

        // Detección de suelo con Raycast hacia abajo
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else 
        {
            Grounded = false;
        }

        // Salto con la tecla W si está tocando el suelo
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Aplicar velocidad al cuerpo rígido (compatible con Unity 2022 y anteriores)
        Rigidbody2D.linearVelocity = new Vector2(Horizontal * Speed, Rigidbody2D.linearVelocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }
}