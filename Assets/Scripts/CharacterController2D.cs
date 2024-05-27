using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 motionVector;
    public Vector2 lastMotionVector;
    public bool moving;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().dialoguePlaying)
        {
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        motionVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));

        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving", moving);

        if (horizontal != 0 || vertical != 0)
        {
            lastMotionVector = new Vector2(
                horizontal,
                vertical
                ).normalized;

            animator.SetFloat("LastHorizontal", horizontal);
            animator.SetFloat("LastVertical", vertical);
        }
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody2d.velocity = motionVector * moveSpeed;
    }
}
