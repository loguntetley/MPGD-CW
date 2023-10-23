using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditor;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class PlayerControllerT : MonoBehaviour
{
    [SerializeField] private float speed, jumpAmount;
    private Vector2 moveValue;
    private Rigidbody rigidBody;
    private PlayerData playerData;

    private void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
    }
    void Update()
    {
        OnJump();
        OnSwapBody();
        gameObject.GetComponent<DeathSystem>().OnDeath();
    }

    private void OnSwapBody()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100) && hit.collider.gameObject.CompareTag("Selectable"))
            {
                Debug.Log(hit.transform.gameObject.name);
                playerData.selectedPlatform = hit.collider.gameObject;
            }
        }
    }
    
    private void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(Vector2.up * jumpAmount, ForceMode.Impulse);
        }
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        rigidBody.AddForce(movement * speed * Time.fixedDeltaTime);
    }
}
