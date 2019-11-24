using System;
using UnityEngine;

public class CharacterActionsController : MonoBehaviour
{
    [Serializable]
    public enum StateChangeType
    {
        HP, Armed
    }
    [Serializable]
    public struct StateChange
    {
        [SerializeField]
        public StateChangeType type;
        [SerializeField]
        public float value;
    }

    public HealthPoints hp;
    public Weapon weapon;

    public GameObject unarmed;

    [SerializeField]
    private bool isArmed;
    public bool IsArmed { get => isArmed; protected set => isArmed = value; }

    private void Start()
    {
        hp.OnChange += Hp_OnChange;

        if (unarmed != null && !IsArmed) SwitchUnarmed(true);
    }

    public void SwitchUnarmed(bool isOn)
    {
        IsArmed = !isOn;
        if (unarmed)
            unarmed.SetActive(isOn);
        if (weapon)
            weapon.gameObject.SetActive(!isOn);
    }

    private void OnDestroy()
    {
        hp.OnChange -= Hp_OnChange;
    }

    public void HitForward()
    {
        if (weapon != null)
        {
            weapon.Attack();
        }
    }

    public void ApplyEffect(StateChange change)
    {
        switch (change.type)
        {
            case StateChangeType.HP:
                hp.Change((int)change.value);
                break;
            case StateChangeType.Armed:
                SwitchUnarmed(change.value <= 0);
                break;
        }
    }

    private void Hp_OnChange(int hpLeft)
    {
        if (hpLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
}
