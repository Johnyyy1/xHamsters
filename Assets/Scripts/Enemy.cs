using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject targetPlayer;
    private Vector3 target;
    private Rigidbody rb;
    public Vector3 lastVelocity;
    public bool canMove = false;

    [SerializeField]
    private int currentHp;


    [SerializeField]
    private int maxHp;


    [SerializeField]
    private float acceleration = 30;

    [SerializeField]
    private float maxSpeed;
    [SerializeField] private float damageMult = 1;

    [SerializeField] private Transform bitContainer;
    [SerializeField] private Transform bladeContainer;
    [SerializeField] private Transform ratchetContainer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        EquipRandomParts();
        currentHp = maxHp;

    }

    private void EquipRandomParts()
    {
        List<BeybladePart> allParts = GameManager.Instance.availableParts;

        if (allParts.Count == 0) return;

        BeybladePart randomBit = GetRandomPartByType(allParts, PartType.Bit);
        BeybladePart randomBlade = GetRandomPartByType(allParts, PartType.Blade);
        BeybladePart randomRatchet = GetRandomPartByType(allParts, PartType.Ratchet);


        ApplyPart(randomBit, bitContainer);
        ApplyPart(randomBlade, bladeContainer);
        ApplyPart(randomRatchet, ratchetContainer);
    }

    private BeybladePart GetRandomPartByType(List<BeybladePart> list, PartType type)
    {
        var filtered = list.Where(p => p.partType == type).ToList();
        if (filtered.Count == 0) return null;
        return filtered[Random.Range(0, filtered.Count)];
    }

    private void ApplyPart(BeybladePart part, Transform container)
    {
        if (part == null) return;

        maxHp += part.hp;
        acceleration += part.acceleration;
        maxSpeed += part.maxSpeed;
        damageMult += part.damageMult;

        if (part.prefab != null && container != null)
        {
            Instantiate(part.prefab, container);
        }
    }


    void Update()
    {
        if (!canMove) return;


        if (targetPlayer != null)
        {
            target = targetPlayer.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (!canMove) return;


        lastVelocity = rb.linearVelocity;
        if (target != null && rb.linearVelocity.magnitude <= maxSpeed)
        {
            Vector3 direction = (target - transform.position).normalized;
            rb.AddForce(direction * acceleration);
        }

        transform.Rotate(Vector3.up * rb.linearVelocity.magnitude);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!canMove) return;

        rb.linearVelocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
        {
            Destroy(gameObject);
        }
    }
}
