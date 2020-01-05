using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public Transform character;
    public Collider2D colliderBox;

    public GameObject interior;
    public GameObject exterior;

    public enum Direction
    {
        UP, Down, Left, Right
    }
    public Direction direction;

    private void Start()
    {
        if (interior == null || exterior == null)
        {
            Debug.LogError("The entrance does not have interior or exterior! Position: " + transform.position);
        }
        if (character == null)
        {
            character = FindObjectOfType<KeyboardMovementController>().transform;
            Debug.LogWarning("The main character is detected automatically. That might slow down the level loading");
        }
        if (colliderBox == null)
        {
            colliderBox = GetComponent<Collider2D>();
            Debug.LogWarning("The collider of the Entrance is detected automatically. That might slow down the level loading");
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject == character.gameObject)
        {
            SwitchState(true);
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject == character.gameObject && IsExiting(trigger))
        {
            SwitchState(false);
        }
    }

    private void SwitchState(bool isOn)
    {
        interior.SetActive(isOn);
        exterior.SetActive(!isOn);
    }

    private bool IsExiting(Collider2D trigger)
    {
        switch (direction)
        {
            case Direction.UP:
                return trigger.bounds.max.y < colliderBox.bounds.max.y;
            case Direction.Down:
                return trigger.bounds.min.y > colliderBox.bounds.min.y;
            case Direction.Left:
                return trigger.bounds.min.x > colliderBox.bounds.min.x;
            case Direction.Right:
                return trigger.bounds.max.x < colliderBox.bounds.max.x;
        }
        return false;
    }
}