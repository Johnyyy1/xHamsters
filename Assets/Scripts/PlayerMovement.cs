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

    public event Action ShowGameover;


    void Start()    
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        currentHp = maxHp;
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
