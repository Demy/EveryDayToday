using UnityEngine;

public class KeyboardMovementController : MonoBehaviour
{
    public float runSpeed = 3f;
    public float rotationSpeed = 3f;
    public Rigidbody2D rb;
    public CharacterActionsController character;
    public NpcController npcController;

    private Vector3 movement;

    private void Start()
    {
        movement = new Vector3();
    }

    void Update()
    {
        movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (Input.GetKey(KeyCode.Space))
        {
            if (character.IsArmed)
                character.HitForward();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            character.SwitchUnarmed(character.IsArmed);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            NPC closest = npcController.GetClosest(character, npcController.talkRange);
            if (closest != null) closest.Talk(character);
        }
    }

    private void FixedUpdate()
    {
        if (movement.sqrMagnitude > 0)
        {
            rb.MovePosition(transform.position + movement.normalized * runSpeed * Time.fixedDeltaTime);
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * rotationSpeed));
        }
    }
}
