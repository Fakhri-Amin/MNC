using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testParticleSystem : MonoBehaviour
{
    public ParticleSystem dust;
    public float speed = 12f;
    private Rigidbody rb;
    float verticalMove;
    float horizontalMove;
    Vector3 input;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    private void ReadInput()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void Move()
    {
        rb.MovePosition(transform.position + input * speed * Time.fixedDeltaTime);

        if (input != Vector3.zero && !dust.isPlaying)
        {
            dust.Play();
        }
        else if (input == Vector3.zero && dust.isPlaying)
        {
            dust.Stop();
        };

        animator.SetBool("isMoving", input != Vector3.zero);
    }

    private void Look()
    {
        if (input == Vector3.zero) return;

        var relative = (transform.position + input) - transform.position;
        var rotation = Quaternion.LookRotation(relative, Vector3.up);

        transform.rotation = rotation;
        
    }

    void CreateDust()
    {
        dust.Play();
    }
}
