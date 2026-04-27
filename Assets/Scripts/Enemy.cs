using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public Action<int, int> changeHp;

    [SerializeField]
    private float acceleration = 30;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    public float damageMult = 1;

    [SerializeField]
    private Transform bitContainer;

    [SerializeField]
    private Transform bladeContainer;

    [SerializeField]
    private Transform ratchetContainer;

    private Image hpBar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hpBar = GameObject.Find("EnemyHpBar").GetComponent<Image>();

        EquipRandomParts();

        currentHp = maxHp;
        UpdateHpBar();
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
        return filtered[UnityEngine.Random.Range(0, filtered.Count)];
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

        rb.linearVelocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal) * 0.5f;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        changeHp?.Invoke(maxHp, currentHp);
        UpdateHpBar();

        if (currentHp <= 0)
        {
            Destroy(gameObject);
            Camera.main.GetComponent<GoToScene>().SwitchToWinScreen();
        }
    }

    private void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = (float)currentHp / maxHp;
        }
    }
}