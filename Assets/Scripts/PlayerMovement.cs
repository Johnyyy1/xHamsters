using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
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


    public Action<int,int> changeHp;

    [Header("Overdrive Settings")]
    [SerializeField] private float overdriveMultiplier = 1.8f;
    [SerializeField] private float healthDrainPerSecond = 15f;

    private bool isOverdriving = false;
    private float drainTimer;


    void Start()    
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
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
        InitializeStats();


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

        if (Input.GetKey(KeyCode.LeftShift) && currentHp > 5) 
        {
            isOverdriving = true;
            HandleHealthDrain();
        }
        else
        {
            isOverdriving = false;
        }
    }

    private void HandleHealthDrain()
    {
        drainTimer += Time.deltaTime;
        if (drainTimer >= 0.1f)
        {
            int damageToTake = Mathf.CeilToInt(healthDrainPerSecond * 0.1f);
            TakeDamage(damageToTake);
            drainTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!canMove) return;


        lastVelocity = rb.linearVelocity;
        float currentMax = isOverdriving ? maxSpeed * overdriveMultiplier : maxSpeed;
        float currentAccel = isOverdriving ? acceleration * overdriveMultiplier : acceleration;

        if (target != null && rb.linearVelocity.magnitude <= currentMax)
        {
            Vector3 direction = (target - transform.position).normalized;
            rb.AddForce(direction * currentAccel);
        }

        float spinSpeed = isOverdriving ? rb.linearVelocity.magnitude * 2f : rb.linearVelocity.magnitude;
        transform.Rotate(Vector3.up * spinSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canMove || !collision.gameObject.CompareTag("Enemy")) return;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        float myImpact = Vector3.Dot(lastVelocity, collision.contacts[0].normal * -1);
        float enemyImpact = Vector3.Dot(enemy.lastVelocity, collision.contacts[0].normal);

        if (myImpact > enemyImpact)
        {
            int damageToDeal = Mathf.CeilToInt(Mathf.Max(0, myImpact) * damageMult * 0.1f);
            enemy.TakeDamage(damageToDeal);
            rb.AddForce(Vector3.Reflect(lastVelocity, collision.contacts[0].normal) * knockback, ForceMode.Impulse);
        }
        else
        {
            int damageToReceive = Mathf.CeilToInt(Mathf.Max(0, enemyImpact) * enemy.damageMult * 0.1f);
            TakeDamage(damageToReceive);
        }
        
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        changeHp?.Invoke(maxHp,currentHp);
        if(currentHp < 0)
        {
            GameOver();
            gameObject.SetActive(false);
        }
    }
    public void GameOver()
    {
        Debug.Log("dead");
       // ShowGameover?.Invoke(); //nefunguje jeste idk proc
       //mainCamera.GetComponent<GoToScene>().SwitchToDeathScreen();

    }
}
