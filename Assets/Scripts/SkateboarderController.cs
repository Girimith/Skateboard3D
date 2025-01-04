using UnityEngine;
using UnityEngine.SceneManagement;

public class SkateboarderController : MonoBehaviour
{
    public float speed = 10f;          
    public float turnSpeed = 5f;       
    public float jumpForce = 10f;       

    private Rigidbody rb;
    private bool isGrounded;

    public Joystick fixedjoystick;

    int checkPointIndex;
    public Transform[] checkPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(checkPointIndex == 1)
        {
            transform.position = checkPoint[1].position;
        }
        if (checkPointIndex == 2)
        {
            transform.position = checkPoint[2].position;
        }
        if (checkPointIndex == 3)
        {
            transform.position = checkPoint[3].position;
        }
        if (checkPointIndex == 4)
        {
            transform.position = checkPoint[4].position;
        }
        
    }

    void Update()
    {
        HandleMovement();

        if(gameObject.transform.position.y < -5)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleMovement()
    {
        float horizontal = fixedjoystick.Horizontal;  
        float vertical = fixedjoystick.Vertical;      

        Vector3 forwardMovement = transform.forward * vertical * speed * Time.deltaTime;

        float turn = horizontal * turnSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + forwardMovement);
        transform.Rotate(0f, turn, 0f);  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
