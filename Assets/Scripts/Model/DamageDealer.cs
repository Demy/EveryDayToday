using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public delegate void KillEvent(CharacterActionsController enemy);
    public KillEvent OnKill = (i) => { };

    public int damage;

    private List<GameObject> collided = new List<GameObject>();

    public void StartHit()
    {
        if (transform.parent.parent.name == "Enemy (2)")
        {
            Debug.Log(transform.parent.parent.name + " Start hit");
        }
        collided.Clear();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collided.Contains(collision.gameObject)) return;
        collided.Add(collision.gameObject);

        HealthPoints hp = collision.gameObject.GetComponent<HealthPoints>();

        if (hp != null)
        {
            hp.Change(-damage);
            if (hp.GetValue() <= 0)
            {
                OnKill(hp.GetComponent<CharacterActionsController>());
            }
        }
    }
}
