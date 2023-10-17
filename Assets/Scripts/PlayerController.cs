using checkPointsManager.runtime;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;

    public float rotateSpeed = 300;
    [Range(1, 2)] public float rotateRatio = 1;
    public Transform playerTransform;
    public Transform eyeViewTransform;
    public float MaxViewAngle = 65f;
    private float tmp_viweRotationOffset;
    Vector3 slient = new Vector3(.0f, 0.0f, .0f);

    public CheckPoint currentCheckpoint;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))// kill the player
        {
            teleportToCheckpoint(currentCheckpoint);
            
        }
    }

    public void OnMove(InputValue value)
    {

        moveValue = value.Get<Vector2>();
        Animator ator = this.GetComponent<Animator>();
        ator.SetBool("run bool", true);
        //if (moveValue == Vector2(0.0f,0.0f))
       
    }

    public void OnJump(InputValue value)
    {
        float jump_button = value.Get<float>();

        if (jump_button > 0)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 300);
        }
    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        movement = transform.TransformDirection(movement);
        if(movement == slient)
        {
            Animator ator = this.GetComponent<Animator>();
            ator.SetBool("run bool", false);
        }
        this.GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime); //movement control
        PlayerRotateControl(); 
        
    }
    private void PlayerRotateControl()
    {
        //camera control
        if (playerTransform == null || eyeViewTransform == null)
        {
            return;
        }
        float offset_x = Input.GetAxis("Mouse X");
        float offset_y = Input.GetAxis("Mouse Y");
        playerTransform.Rotate(Vector3.up * (offset_x * rotateSpeed * rotateRatio * Time.fixedDeltaTime));
        tmp_viweRotationOffset -= offset_y * rotateSpeed * rotateRatio * Time.fixedDeltaTime;
        tmp_viweRotationOffset = Mathf.Clamp(tmp_viweRotationOffset, -MaxViewAngle, MaxViewAngle);
        Quaternion EyeLocalQuaternion = Quaternion.Euler(new Vector3(tmp_viweRotationOffset,
            eyeViewTransform.localEulerAngles.y,
            eyeViewTransform.localEulerAngles.z));
        eyeViewTransform.localRotation = EyeLocalQuaternion;
    }

    
    public void teleportToCheckpoint(CheckPoint checkpoint = null)
    {
        if (checkpoint != null && currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.transform.position;
            transform.rotation = currentCheckpoint.transform.rotation;
        }
        else if (checkpoint != null)
        {
            transform.position = checkpoint.transform.position;
            transform.rotation = checkpoint.transform.rotation;
        }
        else
        {
            throw new System.InvalidOperationException("There is no 'currentCheckpoint' assign in the 'Player_Checkpoint' component on " + gameObject.name);
        }
    }

    
}

