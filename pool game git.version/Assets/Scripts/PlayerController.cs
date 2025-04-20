using System.Collections.Specialized;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
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
        if (count == 3)
        {
            winTextObject.SetActive(true);
        }

        if (count == -1)
        {
            loseTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement*speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") && this.gameObject.CompareTag("Danger"))
        {
            //other.gameObject.SetActive(false);
            count -= 1;
            this.transform.position = new Vector3(0f, 1f, 0f);
            SetCountText();
        }
        else
        {
            this.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
    }

}
