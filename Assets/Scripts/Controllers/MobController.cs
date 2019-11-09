using System;
using UnityEngine;

public class MobController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform target;
    public float speed;
    public float rotationSpeed;
    public float sigthDist;

    public CharacterActionsController actions;

    private float sqrSightDist;
    private float sqrReach;

    private void Start()
    {
        sqrSightDist = sigthDist * sigthDist;
        sqrReach = actions.weapon.reach * actions.weapon.reach;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Pursue();
        }
    }

    private void Pursue()
    {
        Vector3 dist = target.position - transform.position;
        float sqrDist = dist.sqrMagnitude;
        if (sqrDist <= sqrSightDist && sqrDist > sqrReach * 0.6f)
        {
            rb.MovePosition(transform.position + dist.normalized * speed * Time.fixedDeltaTime);
            float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * rotationSpeed));
        }
        if (sqrDist <= sqrReach * 1.1f)
        {
            actions.HitForward();
        }
    }
}
