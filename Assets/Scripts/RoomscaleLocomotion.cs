using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RoomscaleLocomotion : MonoBehaviour
{
    public Rigidbody rollerBall;
    public float torqueMultiplier = 10.0f;
    public float dampingFactor = 0.1f; // Factor to control the smoothness of the response

    // Update is called once per frame
    void Update()
    {
        InputDevices
          .GetDeviceAtXRNode(XRNode.Head)
          .TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 cameraVelocity);

  
    }
}
