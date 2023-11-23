using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class PlayerControllerT : MonoBehaviour
{
    [SerializeField] private float speed, global_speed, jumpAmount, velocityCap = 100;
    private Vector2 moveValue;
    private Rigidbody rigidBody;
    private PlayerData playerData;
    private bool isGrounded = true;
    public float fullspeed = 4;
    private bool high = false;


    private void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
    }
    void Update()
    {
        jumpimprove();
    }

    void OnPlatformselection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30) && hit.collider.gameObject.CompareTag("Selectable"))
        {
            Debug.Log(hit.transform.gameObject.name);
            playerData.selectedPlatform = hit.collider.gameObject;
        }
    }

    private void OnJump()
    {
        if (isGrounded)
        {
            rigidBody.AddForce(Vector2.up * jumpAmount, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        movement = transform.TransformDirection(movement);
        Player_movement_improvement();
        rigidBody.AddForce(movement * speed * Time.fixedDeltaTime);
        if (rigidBody.velocity.sqrMagnitude > velocityCap)
            rigidBody.velocity *= 0.99f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "DeathPlatform" || other.gameObject.tag == "PermanentDeathPlatform" || other.gameObject.tag == "Selectable")
        {
            isGrounded = true;
        }
    }
    private void Player_movement_improvement()
    {
        Vector3 current_Velocity = rigidBody.velocity;
        if (current_Velocity.x - moveValue.x > current_Velocity.x || current_Velocity.y - moveValue.y > current_Velocity.y)
        {
            speed = global_speed;
        }
        else
        {
            speed = global_speed / 1.5f;
        }
    }
    private void jumpimprove()
    {
        if (rigidBody.velocity.y > 3f)
        {
            high = true;
        }
        if (rigidBody.velocity.y < 2.5f && high)
        {
            Physics.gravity = new Vector3(0, -9.8f * fullspeed, 0);
            high = false;
        }
        if (isGrounded)
        {
            Physics.gravity = new Vector3(0, -12f, 0);
        }
    }
}
