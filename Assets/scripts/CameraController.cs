using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float sensivity = 2f;
    public float minXAngle = -30f;
    public float maxXAngle = 30f;
    public float smoothSpeed = 10f;
    public Camera mainCam;
    public float normalFOV = 60f;
    public float aimFOV = 35f;
    public float aimSpeed = 10f;
    public Vector3 cameraOffset = new Vector3(0.8f, 1.5f, -3f);

    private float rotationX = 0f;
    private float rotationY = 0f;
    private bool isAiming = false;
    private bool canLook = true;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        if (!canLook) return;

        float mouseX = Input.GetAxis("Mouse X") * sensivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minXAngle, maxXAngle);

        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        playerTransform.rotation = Quaternion.Slerp(
            playerTransform.rotation,
            Quaternion.Euler(0f, rotationY, 0f),
            smoothSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        transform.position = playerTransform.position + targetRotation * cameraOffset;

        isAiming = Mouse.current.rightButton.isPressed;
        float targetFOV = isAiming ? aimFOV : normalFOV;
        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFOV, Time.deltaTime * aimSpeed);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canLook = true;
    }

    // ✅ MỞ KHÓA CHUỘT
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canLook = false;
    }
}