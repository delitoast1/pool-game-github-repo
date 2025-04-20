using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // Reference to the player GameObject.
    public GameObject player;

    // The distance between the camera and the player.
    private Vector3 offset;
    public float speed = 15.0f; //���y ��t
    public float forceSpd = 1.0f; //���y �W�O �t��
    private float force = 0.0f; //���y �w�g�W�F�h�֤O ���j�p
    public float distance = 6.0f; //��v�� �� ���y �Z�� ��l��
    public float xSpeed = 120.0f; //�ƹ����k���ʳt��
    public float ySpeed = 120.0f; //�ƹ��W�U���ʳt��
    public float yMinLimit = -20f; //�ƹ��W�U ����� �U��
    public float yMaxLimit = 80f; //�ƹ��W�U ����� �W��
    public float distanceMin = .5f; //�u�� �� ��v�� �� ���y �Z���U��
    public float distanceMax = 15f; //�u�� �� ��v�� �� ���y �Z���W��
    private Rigidbody rbody;
    float x = 0.0f;
    float y = 0.0f;
    // Use this for initialization
    // Start is called before the first frame update.

    public Slider slider;
    void Start()
    {
        //��v����m - ���y��m = �۹��m
        offset = transform.position - player.transform.position;
        Vector3 angles = transform.eulerAngles; //��v������
        x = angles.y;
        y = angles.x;
        rbody = player.GetComponent<Rigidbody>();

        slider.value = 0;
    }

    // LateUpdate is called once per frame after all Update functions have been completed.
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(0)) 
    {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit); // ���� ���� �ɥ��d��
                                                     //¶ Y �b �O ¶�y��A¶ X �b �O �ɥ�
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            distance = Mathf.Clamp( // ���� �u�� �� ���� ���ʽd��
            distance - Input.GetAxis("Mouse ScrollWheel") * 5,
            distanceMin, distanceMax);
            // (�u Z �b �e�Ჾ�ʡ^
            
   
    Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            offset = rotation * negDistance; //�̷s���סA�Z�� ���s��۹��m
            transform.rotation = rotation; // ��v�� �s����
        }
        // ��v���s��m = �s�۹��m + ���y��m
        transform.position = player.transform.position + offset;
        if (Input.GetMouseButton(1)) // ���ƹ��k�� ���� �W�O
        {
            force += Time.deltaTime * forceSpd; // �j�p�M�ɶ�������
            slider.value = force;
        }
        else if (Input.GetMouseButtonUp(1)) // ���ƹ��k�� ��} �o�g
        {
            //�����ݪ���V the direction of camera(eye)�G
            // Camera.main.transform.forward
            Vector3 movement = Camera.main.transform.forward;
            movement.y = 0.0f; // no vertical movement ���W�U����
                               //�O�q�Ҧ� impulse:�ĤO�Aspeed�G��t�j�p
            rbody.AddForce(movement * speed * force, ForceMode.Impulse);
            force = 0.0f; // �O�q�κ��k 0�A�ǳƤU�����s�W�O
            slider.value = 0;
        }
    }
    public static float ClampAngle(float angle, float min, float max)
    { // �ΤW�U�� ����
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}


