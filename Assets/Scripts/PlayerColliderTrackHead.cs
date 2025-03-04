using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderTrackHead : MonoBehaviour
{
    public Transform headPosition;

    private Transform playerTransform;
    private CapsuleCollider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCollider.height = headPosition.localPosition.y;
        playerCollider.center = new Vector3(headPosition.localPosition.x, playerCollider.height / 2, headPosition.localPosition.z);
    }
}
