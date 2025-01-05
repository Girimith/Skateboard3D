using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SkateboarderController : MonoBehaviour
{
    public float speed = 10f;          
    public float turnSpeed = 5f;       
    public float jumpForce = 10f;       

    private Rigidbody rb;
    private bool isGrounded;

    public LayerMask groundLayer;
    public float checkDistance = 1.1f;   
    public WheelCollider[] wheelColliders;

    public Joystick fixedjoystick;

    int checkPointIndex;
    public Transform[] checkPoint;

    public TextMeshProUGUI timer;
    public float remainingTime;


    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        
    }

    void CheckPosition()
    {
        if(checkPointIndex == 0)
        {
            SceneManager.LoadScene(0);
        }

        if (checkPointIndex == 1)
        {
            transform.position = checkPoint[0].position;
            transform.rotation = checkPoint[0].rotation;

        }
        if (checkPointIndex == 2)
        {
            transform.position = checkPoint[1].position;
            transform.rotation = checkPoint[1].rotation;

        }
        if (checkPointIndex == 3)
        {
            transform.position = checkPoint[2].position;
            transform.rotation = checkPoint[2].rotation;

        }
        if (checkPointIndex == 4)
        {
            transform.position = checkPoint[3].position;
            transform.rotation = checkPoint[3].rotation;

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log(checkPointIndex);
            CheckPosition();
        }
        if (other.gameObject.tag == "CheckPoint")
        {
            checkPointIndex++;
        }
        if (other.gameObject.tag == "GameComplete")
        {
            UiManager.instance.bg.SetActive(true);
            UiManager.instance.completePanel.SetActive(true);
        }

        
    }

    void Update()
    {
        HandleMovement();

        isGrounded = CheckIfGrounded();

        if (UiManager.instance.gameStart)
        {
            if (remainingTime > 0)
            {

                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;

                UiManager.instance.bg.SetActive(true);
                UiManager.instance.gameOverPanel.SetActive(true);
            }
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }

        if (gameObject.transform.position.y < -5)
        {
            CheckPosition();
        }
    }

    private bool CheckIfGrounded()
    {
        // Check if any wheel is grounded by casting a ray from each wheel's position
        foreach (var wheelCollider in wheelColliders)
        {
            if (wheelCollider.isGrounded)
            {
                return true; // At least one wheel is touching the ground
            }
          
        }
        return false;
    }


    public void Jump()
    {
        if (isGrounded)
        {
            rb.mass = 1;
            rb.transform.GetChild(0).GetComponent<WheelCollider>().enabled = false;
            rb.transform.GetChild(1).GetComponent<WheelCollider>().enabled = false;
            rb.transform.GetChild(2).GetComponent<WheelCollider>().enabled = false;
            rb.transform.GetChild(3).GetComponent<WheelCollider>().enabled = false;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            Invoke("UpdateWheelPositions", 1f);
        }
    }

    private void UpdateWheelPositions()
    {
        rb.mass = 1000;
        rb.transform.GetChild(0).GetComponent<WheelCollider>().enabled = true;
        rb.transform.GetChild(1).GetComponent<WheelCollider>().enabled = true;
        rb.transform.GetChild(2).GetComponent<WheelCollider>().enabled = true;
        rb.transform.GetChild(3).GetComponent<WheelCollider>().enabled = true;
    }

    void HandleMovement()
    {
#if UNITY_EDITOR
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
#else
        float horizontal = fixedjoystick.Horizontal;  
        float vertical = fixedjoystick.Vertical;
#endif


        Vector3 forwardMovement = transform.forward * vertical * speed * Time.deltaTime;

        float turn = horizontal * turnSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + forwardMovement);
        transform.Rotate(0f, turn, 0f);  
    }


    

}
