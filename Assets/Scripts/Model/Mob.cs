using UnityEngine;
using System.Collections;
using System;

public class Mob : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float rotationSpeed;
    public float sigthDist;

    public CharacterActionsController actions;

    private int id;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    public void AssignId(int id)
    {
        this.id = id;
    }

    public int getId()
    {
        return id;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + targetPosition.normalized * speed * Time.fixedDeltaTime);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }

    internal void MoveAndHit(Vector3 position, Quaternion rotation, bool hit)
    {
        if (hit) actions.HitForward();
        targetPosition = position;
        targetRotation = rotation;
    }
}
