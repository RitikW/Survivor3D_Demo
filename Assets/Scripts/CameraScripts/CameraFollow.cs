﻿using UnityEngine;
namespace CameraScripts
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject target; 
        public float damping = 0.5f;
        public Vector3 offset;

        void FixedUpdate()
        {
            if (target != null)
            {
                Vector3 desiredPosition = target.transform.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, damping);
                transform.position = smoothedPosition;

                transform.LookAt(target.transform);
            }
        }
    }
}
