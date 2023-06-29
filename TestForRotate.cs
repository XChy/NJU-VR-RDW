using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForRotate : MonoBehaviour
{
    public GameObject VRController;
    public GameObject VRTracker;

    private float prevYRotation = 0.0f;
    private Quaternion prevRotation;

    void Start()
    {
        prevYRotation = VRController.transform.eulerAngles.y;
        prevRotation = VRController.transform.rotation;
    }

    void Update()
    {
        Quaternion currentRotation = VRController.transform.rotation;

        float currentYRotation = VRController.transform.eulerAngles.y;
        float deltaY = currentYRotation - prevYRotation;
        if(deltaY > 180)
        {
            deltaY -= 360;
        }
        if(deltaY < -180)
        {
            deltaY += 360;
        }

        // 计算虚拟视角需要旋转的角度
        float virtualRotation = deltaY / 2.0f;
        float newY = VRTracker.transform.eulerAngles.y + virtualRotation;


        // 创建一个新的欧拉角，并将虚拟旋转应用于y轴
        Vector3 newEulerAngles = new Vector3(
            VRTracker.transform.eulerAngles.x,
            newY,
            VRTracker.transform.eulerAngles.z
        );

        // 将新的欧拉角应用于VRTracker对象
        VRTracker.transform.eulerAngles = newEulerAngles;

        // 更新prevYRotation
        prevYRotation = currentYRotation;
        prevRotation = currentRotation;
    }
}