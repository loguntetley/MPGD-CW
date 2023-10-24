using checkPointsManager.runtime;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /*movement system*/
    public Vector2 moveValue;
    public float speed;
    /*Camera_system*/
    public float rotateSpeed = 400;
    [Range(1, 2)] public float rotateRatio = 1;
    public Transform playerTransform;
    public Transform eyeViewTransform;
    public GameObject[] Cameras;
    private bool Camera_is_firstperson = true; 
    public float MaxViewAngle = 65f;
    private float tmp_viweRotationOffset = 0 ;
    /*Use to switch the animator*/
    Vector3 slient = new Vector3(.0f, 0.0f, .0f);
    /*jump scan*/
    public static bool readytoJump = true;
    public CheckPoint currentCheckpoint;
    

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyInputScan();   
    }
    void FixedUpdate()
    {
        movement_control();
        PlayerRotateControl();

    }
    /*input system*/
    public void OnMove(InputValue value)
    {

        moveValue = value.Get<Vector2>();
        Animator ator = this.GetComponent<Animator>();
        ator.SetBool("run bool", true);
        //if (moveValue == Vector2(0.0f,0.0f))
       
    }
    
    public void OnJump()
    {
        if (  readytoJump == true)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 300);
        }
    }

    public void movement_control()
    {
        //use rigidbody to move the player
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        movement = transform.TransformDirection(movement);
        if (movement == slient)
        {
            Animator ator = this.GetComponent<Animator>();
            ator.SetBool("run bool", false);
        }
        this.GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime); 
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
        if(Camera_is_firstperson)
        {
            tmp_viweRotationOffset -= offset_y * rotateSpeed * rotateRatio * Time.fixedDeltaTime;
            tmp_viweRotationOffset = Mathf.Clamp(tmp_viweRotationOffset, -MaxViewAngle, MaxViewAngle);
        }
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

    public void KeyInputScan()
    {
        if (Input.GetKeyDown(KeyCode.K))// kill the player
        {
            teleportToCheckpoint(currentCheckpoint);

        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Camera_switch();
        }
    }

    public void Camera_switch()
    {
        if(Camera_is_firstperson)
        {
            eyeViewTransform = Cameras[1].transform;
            Cameras[0].SetActive(false);
            Cameras[1].SetActive(true);
            Camera_is_firstperson = false;
        }
        else
        {
            eyeViewTransform = Cameras[0].transform;
            Cameras[0].SetActive(true);
            Cameras[1].SetActive(false);
            Camera_is_firstperson = true;

        }
    }


}

