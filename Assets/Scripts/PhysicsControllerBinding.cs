using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsControllerBinding : MonoBehaviour
{
    public Transform controllerRig;
    public Transform cameraOffset;
    public Transform followPosition;

    // Update is called once per frame
    void Update()
    {
        controllerRig.position = followPosition.position - cameraOffset.localPosition;
    }
}
