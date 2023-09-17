using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BEAN_PLAYER : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    private int superJumpsRemaining;
    public GameObject PanelLose;
    public GameObject PanelWin;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Finish()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }
    //Once every phisics update
    private void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector3(horizontalInput * 2, rigidBodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
        if (other.tag == "Death")
        {
            Lose();
        }
        if (other.tag == "Portal")
        {
            Finish();
        }
    }

    
}
