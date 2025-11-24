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


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHp = maxHp;

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
