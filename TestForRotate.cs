using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TestForRotate : MonoBehaviour
{
    public GameObject VRController;
    public GameObject VRTracker;
    public GameObject wallMarker;

    private float prevYRotation = 0.0f;
    private Quaternion prevRotation;

    private OVRBoundary ovrBoundary;

    void Start()
    {
        prevYRotation = VRController.transform.eulerAngles.y;
        prevRotation = VRController.transform.rotation;

        get_boundary_vertices();
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

    public void get_boundary_vertices()
    {
        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);

        // make sure I actually have a subsystem loaded
        if (subsystems.Count > 0)
        {
            // create a List of Vec3 that will be filled with the vertices
            List<Vector3> boundaryPoints = new List<Vector3>();

            // if this returns true, then the subsystems supports a boundary and it should have filled our list with them
            if (subsystems[0].TryGetBoundaryPoints(boundaryPoints))
            {
                foreach (Vector3 pos in boundaryPoints)
                {
                    // TODO: do something sensible with the points, not this
                    Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), pos, Quaternion.identity);
                    Debug.Log("GET POINT:"+pos);
                }
            }
        }
    }
}
