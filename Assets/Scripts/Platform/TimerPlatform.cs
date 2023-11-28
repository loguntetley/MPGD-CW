using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//This platform will vanish few seconds after the player touch it.
public class TimerPlatform : MonoBehaviour
{
    private float delay = 2f;// Change the delay time here.
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private float animationTime = 2f;//平台溶解所需要的时间。The time required for the platform to dissolve.
    private float t = 0.0f;//计时器，用于计算平台溶解动画。Timer used to calculate the platform dissolve animation.
    private int platformState = 3;//由于动画必须要写在update函数里，因此需要用状态来区分是否执行动画。Since the animation must be written in the update function, the state needs to be used to distinguish whether to execute the animation.
    private float value; // 用于设定材质动画的值。Value used to animate the material.
    private Material[] mats;//物体的材质对象。The object's material object.
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
            //状态0表示平台消失，需要展示溶解动画。State 0 means that the platform disappears and a dissolving animation needs to be displayed.
            t += Time.deltaTime;
            value = Mathf.Lerp(0f, 1f, t / animationTime);
            mats[0].SetFloat("_Cutoff", value);
            //Debug.Log("0: " + t + ", " + value);
        }
        if (platformState == 1)
        {
            //状态1表示平台重建，需要展示反向的溶解动画。State 1 indicates that the platform is rebuilt and needs to show the reverse dissolution animation.
            t += Time.deltaTime;
            value = Mathf.Lerp(1f, 0f, t / animationTime);
            mats[0].SetFloat("_Cutoff", value);
            //Debug.Log("1: " + t + ", " + value);
        }

        // Unity does not allow meshRenderer.materials[0]...
        meshRenderer.materials = mats;

        if (t > animationTime)
        {
            // 重置计时器并设置为状态3，状态3只是为了不执行上面两个动画。Reset the timer and set it to state 3, state 3 is just to not execute the above two animations.
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
        //原来是将物体设置为禁用的方式让玩家掉落，但是要想让平台显示出溶解的动画效果就不能禁用它，因此我使用了开启/关闭物体物理组件的方式让玩家掉落。
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
