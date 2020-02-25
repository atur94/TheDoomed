using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class FollowScript : MonoBehaviour
{
    public Transform PlayerTransform;
    public float Forward;
    public float Up;
    private void LateUpdate()
    {
        transform.position = PlayerTransform.position + Vector3.forward * Forward + Vector3.up * Up;
    }
}
