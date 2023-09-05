using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class TestForRotate : MonoBehaviour
{
    public GameObject VRController;
    public GameObject VRTracker;

    private float prevYRotation = 0.0f;


    void Start()
    {
        prevYRotation = VRController.transform.eulerAngles.y;

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

        // 将新的欧拉角应用于VRTracker对象
        VRTracker.transform.RotateAround(VRController.transform.position, Vector3.up, virtualRotation);
        
        // 更新prevYRotation
        prevYRotation = currentYRotation;
    }

    //public void get_boundary_vertices()
    //{
    //    List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
    //    SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);

    //    // make sure I actually have a subsystem loaded
    //    if (subsystems.Count > 0)
    //    {
    //        // create a List of Vec3 that will be filled with the vertices
    //        List<Vector3> boundaryPoints = new List<Vector3>();

    //        // if this returns true, then the subsystems supports a boundary and it should have filled our list with them
    //        if (subsystems[0].TryGetBoundaryPoints(boundaryPoints))
    //        {
    //            foreach (Vector3 pos in boundaryPoints)
    //            {
    //                // TODO: do something sensible with the points, not this
    //                Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), pos, Quaternion.identity);
    //                Debug.Log("GET POINT:" + pos);
    //            }
    //        }
    //    }

    //}
    public void get_boundary_vertices()
    {
        

        /*List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);
        Debug.Log(subsystems.Count);
        if (subsystems.Count > 0)
        {
            Debug.Log("haah");
            List<Vector3> boundaryPoints = new List<Vector3>();

            if (subsystems[0].TryGetBoundaryPoints(boundaryPoints))
            {
                Debug.Log("wojinlaila");
                foreach (Vector3 pos in boundaryPoints)
                {
                    Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), pos, Quaternion.identity);
                    Debug.Log("GET POINT:" + pos);
                }
            }
            else
            {
                Debug.Log("Unable to get boundary points from XRInputSubsystem.");
            }
        }
        else
        {
            Debug.Log("No XRInputSubsystem instances found.");
        }
    }*/
  
    
        Debug.Log("start Fuc boundary");

        //if (OVRManager.boundary.GetConfigured())
        //{
            if (OVRManager.boundary != null)
            {
                    Vector3[] boundaryPoints = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea); 
            if (boundaryPoints.Length != 0)
            {
                Debug.Log("succeed");
            }
            }
            else
            {
                Debug.Log("null pointer");
            }
           
        //}
        //else
        //{
        //    Debug.Log("not in");
        //}
    }
}