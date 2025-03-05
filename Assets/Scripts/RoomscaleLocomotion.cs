using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RoomscaleLocomotion : MonoBehaviour
{
    public Rigidbody rollerBall;

    // Update is called once per fr
    void FixedUpdate()
    {
        InputDevices
          .GetDeviceAtXRNode(XRNode.Head)
          .TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 cameraVelocity);

    }
}
