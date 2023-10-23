using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public GameObject player;
    private Vector3 offset;
    [SerializeField] float speed = 5.0f;
    [SerializeField] private float mouseSensitivity = 3.0f;
    private float rotationY;
    private float rotationX;
    private float distance;
    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector2 rotationXMinMax = new Vector2(-40, 40);

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
        distance = Vector3.Distance(player.transform.position, this.transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        CameraRotator();
    }

    private void CameraRotator()
    {
        if (Input.GetMouseButton(1)) 
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationY += mouseX;
            rotationX += mouseY;

            rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);
            Vector3 nextRotation = new Vector3(rotationX, rotationY);
            currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
            transform.localEulerAngles = currentRotation;
            transform.position = player.transform.position - transform.forward * distance;
        }

    }
}
