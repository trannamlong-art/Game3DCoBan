using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    private Animator animator;

    public float mouseSensitivity = 100f;
    private float rotationY = 0f;

    private bool throwing = false;
    public float throwTime = 5.0f;  // thời gian animation throw

    IEnumerator ThrowRoutine()
    {
        throwing = true;
        animator.SetBool("isThrowing", true);

        yield return new WaitForSeconds(throwTime);

        animator.SetBool("isThrowing", false);
        throwing = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
   
    void Update()
    {
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            animator.SetBool("Shoot", true);
            Debug.Log("Shoot");
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            animator.SetBool("Shoot", false);
        }

        if (Keyboard.current.rKey.wasPressedThisFrame && !throwing)
        {
            StartCoroutine(ThrowRoutine());
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = transform.right * moveInput.x + transform.forward * moveInput.y;

        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);

        animator.SetFloat("MoveX", moveInput.x);   // A = -1, D = +1
        animator.SetFloat("MoveY", moveInput.y);   // S = -1, W = +1
    }
    public void OnMove(InputValue InputValue)
    {
        moveInput = InputValue.Get<Vector2>();
    }
}
