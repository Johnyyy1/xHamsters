using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject player;
    private Vector3 target;
    private Rigidbody rb;

    [SerializeField]
    private float speed = 30;


    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target - transform.position).normalized;
            rb.AddForce(direction * speed);
        }

        transform.Rotate(Vector3.up * rb.linearVelocity.magnitude);
    }
}
