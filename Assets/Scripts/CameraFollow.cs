using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public GameObject player;
    private Vector3 offset;
    [SerializeField] private float mouseSensitivity = 3.0f;
    private float rotationY;
    private float rotationX;
    private float distance;
    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private Vector2 rotationXMinMax = new Vector2(0, 40);
    private Vector3 camera_current_Rotate;
    private float maxsize = 100.0f;//camera size control
    private float minsize = 20.0f;
    private float maxSD = 20.0f;
    private float minSD = 1f;
    [Header("Distance between camera and player")]
    public float freeDistance = 5f;
    private Vector3 m_rayDirection;
    Transform m_transform;
    private Ray m_ray;
    private RaycastHit m_hit;
    private Vector3 CubePosition;
    private float proportion = 1f;  //rate of dictance of the camera
    // Start is called before the first frame update
    private void Awake()
    {
        m_transform = transform;
        CubePosition = m_transform.position;


    }
    void Start()
    {
        //offset = transform.position;
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        CameraRotator(true);
        transform.position = player.transform.position + offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.GetComponent<MeshRenderer>().enabled)
        {
            CameraRotator(false);
            //transform.position = player.transform.position + offset;
            //transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref smoothVelocity, smoothTime);
            Zoom();
            PreventThroughWall();
        }
    }

    private void CameraRotator(bool start)
    {
        if (Input.GetMouseButton(1) || start)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotationY += mouseX;
            rotationX += mouseY;

            rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);
            Vector3 player_nextRotation = new Vector3(0f, rotationY);

            Vector3 nextRotation = new Vector3(rotationX, rotationY);

            currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
            transform.localEulerAngles = currentRotation;
            player.transform.localEulerAngles = player_nextRotation;
            //transform.position = Vector3.SmoothDamp(transform.position, player.transform.position - transform.forward * distance, ref smoothVelocity, smoothTime);
            transform.position = player.transform.position - transform.forward * distance;
            offset = transform.position - player.transform.position;
            offset = offset.normalized * freeDistance * proportion;
        }

    }
    void Zoom()
    {
        //ʵ�ֻ����϶�
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= maxsize)//���ŵķ�Χ
            {
                Camera.main.fieldOfView += 2;
            }
            if (Camera.main.orthographicSize <= maxSD)
            {
                Camera.main.orthographicSize += 0.5f;
            }
        }

        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView >= minsize)
            {
                Camera.main.fieldOfView -= 2;
            }
            if (Camera.main.orthographicSize >= minSD)
            {
                Camera.main.orthographicSize -= 0.5f;
            }
        }

    }
    private void PreventThroughWall()
    {
        //������������λ�÷���һ������ĵ�����
        m_rayDirection = m_transform.position - player.transform.position;
        //���������淶������������ģΪ1
        m_rayDirection.Normalize();
        //������ĸ���Ŀ�����������һ������Ϊ���Ĭ�Ͼ��������
        m_ray = new Ray(player.transform.position, m_rayDirection * freeDistance);

        //������Լ�⵽��ײ���壬������ײ�����tagδ"Cube"
        if (Physics.Raycast(m_ray, out m_hit) && !m_hit.collider.tag.Equals("cube"))
        {
            //��ȡ����������
            CubePosition = new Vector3(m_hit.point.x, m_hit.point.y, m_hit.point.z);
            //��ȡ����������ľ���
            float distance = Vector3.Distance(CubePosition, player.transform.position);
            //��������ľ������
            proportion = Mathf.Min(1.0f, distance / freeDistance);
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, 5f);//����λ��
        }
        else
        {
            proportion = 1.0f;
            offset = offset.normalized * freeDistance;
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, 1f);//����λ��
            if (false)
            {
                freeDistance -= Input.GetAxis("Mouse ScrollWheel") * mouseSensitivity * Time.deltaTime;
                freeDistance = Mathf.Clamp(freeDistance, minsize, maxsize);
            }
        }
    }

}
