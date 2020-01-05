using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform target;
    public float speed;
    public float rotationSpeed;
    public float sigthDist;

    public CharacterActionsController actions;

    public void MakeAction ()
    {
        Pursue();
    }

    public void Decide ()
    {

    }

    private void Pursue()
    {
        float sqrSightDist = sigthDist * sigthDist;
        float sqrReach = actions.weapon.reach * actions.weapon.reach;
        Vector3 dist = target.position - transform.position;
        float sqrDist = dist.sqrMagnitude;
        if (sqrDist <= sqrSightDist && sqrDist > sqrReach * 0.6f)
        {
            if (!actions.IsArmed) actions.SwitchUnarmed(false);
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
