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

        // ���������ӽ���Ҫ��ת�ĽǶ�
        float virtualRotation = deltaY / 2.0f;
        float newY = VRTracker.transform.eulerAngles.y + virtualRotation;


        // ����һ���µ�ŷ���ǣ�����������תӦ����y��
        Vector3 newEulerAngles = new Vector3(
            VRTracker.transform.eulerAngles.x,
            newY,
            VRTracker.transform.eulerAngles.z
        );

        // ���µ�ŷ����Ӧ����VRTracker����
        VRTracker.transform.eulerAngles = newEulerAngles;

        // ����prevYRotation
        prevYRotation = currentYRotation;
        prevRotation = currentRotation;
    }
}