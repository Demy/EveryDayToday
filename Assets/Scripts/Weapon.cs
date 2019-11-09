using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator animator;
    public float cooldownTime;
    public float reach;
    public DamageDealer weaponDamageDealer;

    private float cooldown;

    public void Attack()
    {
        if (cooldown <= 0)
        {
            animator.SetTrigger("Hit");
            weaponDamageDealer.StartHit();
            cooldown = cooldownTime;
        }
    }

    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
