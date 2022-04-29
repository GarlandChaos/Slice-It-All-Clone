using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowController : MonoBehaviour
{
    [SerializeField]
    Transform targetToFollow = null;

    private void LateUpdate()
    {
        transform.position = targetToFollow.position;
    }
}
