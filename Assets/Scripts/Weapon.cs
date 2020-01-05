using UnityEngine;

public class Weapon : MonoBehaviour
{
    public delegate void KillEvent(CharacterActionsController target);
    public KillEvent OnKill = (i) => { };

    public Animator animator;
    public float cooldownTime;
    public float reach;
    public DamageDealer weaponDamageDealer;

    private float cooldown;

    private void Start()
    {
        weaponDamageDealer.OnKill += DamageDealer_OnKill;
    }

    private void OnDestroy()
    {
        weaponDamageDealer.OnKill -= DamageDealer_OnKill;
    }

    private void DamageDealer_OnKill (CharacterActionsController target)
    {
        OnKill(target);
    }

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
