using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float jumpForce = 7f;
    private bool isGrounded;

    private Rigidbody rb;
    private Animator anim;

    private int isWalkingHash = Animator.StringToHash("isWalking");
    private int jumpTriggerHash = Animator.StringToHash("Jump");

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Keyboard.current == null) return;
        float moveX = 0f;
        float moveZ = 0f;
    if(Keyboard.current.wKey.isPressed) moveZ += 1f;
    if(Keyboard.current.sKey.isPressed) moveZ -= 1f;
    if(Keyboard.current.aKey.isPressed) moveX -= 1f;
    if(Keyboard.current.dKey.isPressed) moveX += 1f;

    Vector3 inputDir = new Vector3(moveX, 0f, moveZ).normalized;
    Vector3 moveDirection = transform.right * inputDir.x + transform.forward * inputDir.z;

        rb.linearVelocity = new Vector3(moveDirection.x * walkSpeed, rb.linearVelocity.y, moveDirection.z * walkSpeed);

        if(isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            if(anim != null)
            {
                anim.ResetTrigger(jumpTriggerHash);
                anim.SetTrigger(jumpTriggerHash);

            }
        }

        if(anim != null)
        {
            bool isWalking = isGrounded && (inputDir.sqrMagnitude > 0f);
            anim.SetBool(isWalkingHash, isWalking);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if(anim != null)
            {
                anim.ResetTrigger(jumpTriggerHash);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
