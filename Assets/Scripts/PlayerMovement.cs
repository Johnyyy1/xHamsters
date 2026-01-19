using System;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 target;
    private Rigidbody rb;
    private Vector3 lastVelocity;
    public bool canMove = false;

    [SerializeField]
    private int currentHp;

    [SerializeField]
    private int maxHp;
    [SerializeField]
    private float acceleration = 30;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float damageMult;
    [SerializeField]
    private float knockback;

    private BeybladePart bit;
    private BeybladePart blade;
    private BeybladePart ratchet;
    private BeybladePart[] parts;

    public event Action ShowGameover;

    [SerializeField]
    private Transform bitContainer;
    [SerializeField]
    private Transform bladeContainer;
    [SerializeField]
    private Transform ratchetContainer;

    void Start()    
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        InitializeStats();
        currentHp = maxHp;

        foreach (BeybladePart part in GameManager.Instance.AddParts())
        {
            switch (part.partType)
            {
                case PartType.Bit:
                    bit = part;
                    break;
                case PartType.Blade:
                    blade = part;
                    break;
                case PartType.Ratchet:
                    ratchet = part;
                    break;
            }
        }

        Instantiate(bit.prefab, bitContainer);
        Instantiate(blade.prefab, bladeContainer);
        Instantiate(ratchet.prefab, ratchetContainer);
    }

    private void InitializeStats()
    {

        parts = new[] { bit,blade,ratchet};

        foreach (var part in parts)
        {
            maxHp += part.hp;
            acceleration += part.acceleration;
            maxSpeed += part.maxSpeed;
            damageMult += part.damageMult;
            knockback += part.knockback;
        }

    }
    
    void Update()
    {
        if (!canMove) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point;
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


        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            collision.gameObject.GetComponent<Enemy>().TakeDamage((int)(lastVelocity.magnitude));
            TakeDamage((int)(enemy.lastVelocity.magnitude));

        }

        rb.linearVelocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if(currentHp < 0)
        {
            GameOver();
            gameObject.SetActive(false);
        }
    }
    public void GameOver()
    {
        Debug.Log("dead");
        ShowGameover?.Invoke(); //nefunguje jeste idk proc
        
    }
}
