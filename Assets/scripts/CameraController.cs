using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform; // Tham chiếu đến Transform của nhân vật người chơi
    public float sensivity = 2f;
    public float minXAngle = -30f;
    public float maxXAngle = 30f;
    public float minYAngle = -360f;
    public float maxYAngle = 360f;
    public float smoothSpeed = 10f;

    public Camera mainCam;
    public float normalFOV = 60f;
    public float aimFOV = 35f;
    public float aimSpeed = 10f;

    private bool isAiming = false;


    private float rotationX = 0f;
    private float rotationY = 0f;

    public Vector3 cameraOffset = new Vector3(0.8f, 1.5f, -3f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float characterOffsetY = 0f;
        float mouseX = Input.GetAxis("Mouse X") * sensivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, minXAngle, maxXAngle);
        rotationY = Mathf.Clamp(rotationY, minYAngle, maxYAngle);

        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        playerTransform.rotation = Quaternion.Slerp(
        playerTransform.rotation,
        Quaternion.Euler(0f, rotationY + characterOffsetY, 0f),
        smoothSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        transform.position = playerTransform.position + targetRotation * cameraOffset;

        // Nhấn chuột phải → ngắm
        if (Mouse.current.rightButton.isPressed)
            isAiming = true;
        else
            isAiming = false;

        // Smooth zoom
        float targetFOV = isAiming ? aimFOV : normalFOV;
        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFOV, Time.deltaTime * aimSpeed);

    }
}
