using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class FollowController : MonoBehaviour
    {
        [SerializeField]
        Transform targetToFollow = null;

        private void LateUpdate()
        {
            transform.position = targetToFollow.position;
        }

        public void ChangeTarget(Transform target)
        {
            targetToFollow = target;
        }
    }
}
