using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform character;
    public float padding;

    private float maxDistX;
    private float maxDistY;

    private void Start()
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight / Screen.height * Screen.width;
        maxDistX = screenWidth * 0.5f - padding;
        maxDistY = screenHeight * 0.5f - padding;
    }

    private void LateUpdate()
    {
        Vector3 dist = character.position - transform.position;
        float newX = transform.position.x;
        float newY = transform.position.y;
        if (dist.x > maxDistX)
        {
            newX = character.position.x - maxDistX;
        }
        if (dist.x < -maxDistX)
        {
            newX = character.position.x + maxDistX;
        }
        if (dist.y > maxDistY)
        {
            newY = character.position.y - maxDistY;
        }
        if (dist.y < -maxDistY)
        {
            newY = character.position.y + maxDistY;
        }
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
