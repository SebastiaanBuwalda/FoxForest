// Smooth towards the target

using UnityEngine;
using System.Collections;

public class DampCamera2D : MonoBehaviour
{
    public Rigidbody2D target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    public Vector3 offSet;

    void Update()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.position;

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + offSet, ref velocity, smoothTime);
    }
}