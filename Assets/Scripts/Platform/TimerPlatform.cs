using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//This platform will vanish few seconds after the player touch it.
public class TimerPlatform : MonoBehaviour
{
    private float delay = 2f;// Change the delay time here.
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private float animationTime = 2f;//ƽ̨�ܽ�����Ҫ��ʱ�䡣The time required for the platform to dissolve.
    private float t = 0.0f;//��ʱ�������ڼ���ƽ̨�ܽ⶯����Timer used to calculate the platform dissolve animation.
    private int platformState = 3;//���ڶ�������Ҫд��update����������Ҫ��״̬�������Ƿ�ִ�ж�����Since the animation must be written in the update function, the state needs to be used to distinguish whether to execute the animation.
    private float value; // �����趨���ʶ�����ֵ��Value used to animate the material.
    private Material[] mats;//����Ĳ��ʶ���The object's material object.
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = this.GetComponent<MeshRenderer>();
        mats = meshRenderer.materials;
    }


    private void Update()
    {
        if (platformState == 0)
        {
            //״̬0��ʾƽ̨��ʧ����Ҫչʾ�ܽ⶯����State 0 means that the platform disappears and a dissolving animation needs to be displayed.
            t += Time.deltaTime;
            value = Mathf.Lerp(0f, 1f, t / animationTime);
            mats[0].SetFloat("_Cutoff", value);
            //Debug.Log("0: " + t + ", " + value);
        }
        if (platformState == 1)
        {
            //״̬1��ʾƽ̨�ؽ�����Ҫչʾ������ܽ⶯����State 1 indicates that the platform is rebuilt and needs to show the reverse dissolution animation.
            t += Time.deltaTime;
            value = Mathf.Lerp(1f, 0f, t / animationTime);
            mats[0].SetFloat("_Cutoff", value);
            //Debug.Log("1: " + t + ", " + value);
        }

        // Unity does not allow meshRenderer.materials[0]...
        meshRenderer.materials = mats;

        if (t > animationTime)
        {
            // ���ü�ʱ��������Ϊ״̬3��״̬3ֻ��Ϊ�˲�ִ����������������Reset the timer and set it to state 3, state 3 is just to not execute the above two animations.
            t = 0f;
            platformState = 3;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Destroying());
        }
    }

    private IEnumerator Destroying()
    {
        Invoke("destroy", delay);
        Invoke("recreate", delay + 3f);
        yield return new WaitForSeconds(delay);
    }
    void destroy()
    {
        //gameObject.SetActive(false);
        //ԭ���ǽ���������Ϊ���õķ�ʽ����ҵ��䣬����Ҫ����ƽ̨��ʾ���ܽ�Ķ���Ч���Ͳ��ܽ������������ʹ���˿���/�ر�������������ķ�ʽ����ҵ��䡣
        //It turns out that the object is set to disabled to make the player fall, but if you want the platform to display the dissolving animation effect, you cannot disable it, so I used the method of turning on/off the physical component of the object to make the player fall.
        boxCollider.enabled = false;
        platformState = 0;
    }
    void recreate()
    {
        //gameObject.SetActive(true);
        boxCollider.enabled = true;
        platformState = 1;
    }
}
