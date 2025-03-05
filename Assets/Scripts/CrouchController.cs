using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchController : MonoBehaviour
{
    public Transform cameraOffset;
    public ConfigurableJoint fenderTorsoJoint;

    // Update is called once per frame
    void Update()
    {
        fenderTorsoJoint.anchor = new Vector3(0, cameraOffset.localPosition.y - 0.8f, 0); // 0.8 is the offset from camera to ground when anchor is 0
    }
}
