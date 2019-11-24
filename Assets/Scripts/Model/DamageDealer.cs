using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage;

    private List<Collider2D> collided = new List<Collider2D>();

    public void StartHit()
    {
        collided.Clear();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collided.Contains(collision.collider)) return;
        collided.Add(collision.collider);

        HealthPoints hp = collision.gameObject.GetComponent<HealthPoints>();
        if (hp != null)
        {
            hp.Change(-damage);
        }
    }
}
