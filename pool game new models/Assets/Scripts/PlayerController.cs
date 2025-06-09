using System.Collections.Specialized;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private Rigidbody rb;
    private static int count;
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static bool countInitialized = false;
    private static bool Initialized = false;
    private static bool islose = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!countInitialized)
        {
            count = 0;
            countInitialized = true;
        }
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        SetCountText();
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count ==20)
        {
            winTextObject.SetActive(true);
        }

        if (count == -1)
        {
            loseTextObject.SetActive(true);
            islose = true;
        }
        
    }

    void FixedUpdate()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetMouseButtonUp(1) && this.gameObject.CompareTag("Danger"))
        {
            //count = 0;
            this.transform.position = new Vector3(2.55f, 4.84f, -0.13f);
            this.transform.rotation = rotation;
            //rb.linearVelocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            //SetCountText();
            Initialized = false;
        }
        if(Input.GetMouseButtonUp(1) && this.gameObject.CompareTag("tallwall1"))
        {
            this.transform.position = new Vector3(2.52f, 2.21f, -1.92f);
            this.transform.rotation = rotation;
        }
        if (Input.GetMouseButtonUp(1) && this.gameObject.CompareTag("tallwall2"))
        {
            this.transform.position = new Vector3(2.52f, 2.21f, 1.8f);
            this.transform.rotation = rotation;
        }
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        // 用來固定走向用der
        Vector3 movement = camRight * movementX + camForward * movementY;

        rb.AddForce(movement * speed);
        if (islose==true && Input.GetMouseButtonUp(1))
        {
            count = 0;
            winTextObject.SetActive(false);
            loseTextObject.SetActive(false);
            SetCountText();
            islose = false;
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp") && this.gameObject.CompareTag("Danger"))
        {
            if (!Initialized)
            {
               
                //other.gameObject.SetActive(false);
                count -= 1;
                //this.transform.position = new Vector3(0f, 1f, 0f);
                SetCountText();
                Initialized = true;
            }
            
        }
        else if(other.gameObject.CompareTag("safe") && this.gameObject.CompareTag("playersafe"))
        {
            winTextObject.SetActive(true);
            //this.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
    }

}
