using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10;
    private bool isOnGround = true;
    public bool gameOver = false;
    public float gravityModifier;
    private Rigidbody playerRb;
    private Animator playerAnim;
    public float xBound = 6.0f;
    public bool isAlive = false;
    public GameObject arrowBox;
    public GameObject arrow;
    public float arrowCooldown = 0.05f;
    private bool canShoot = true;
    private GameManager gameManager;
    private Vector3 gravity = Physics.gravity;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
        ShootArrow();
    }
    
    private void MovePlayer() {
        if (isAlive) {
            playerAnim.SetBool("Moving", true);
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.forward * speed * horizontalInput * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;

                playerAnim.SetInteger("Jumping", 1);
                playerAnim.SetInteger("Trigger Number", 1);
                playerAnim.SetTrigger("Trigger");

            }
        }
    }

    private void ConstrainPlayerPosition() {
        if (transform.position.x < -xBound) {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBound) {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            playerAnim.Play("Land");
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("Enemy")) {
            gameOver = true;
            isAlive = false;
            playerAnim.Play("Death");
            Physics.gravity = gravity;
            gameManager.GameOver();
        }
    }

    private void ShootArrow() {
        if (Input.GetKeyDown(KeyCode.L) && canShoot && !gameOver) {
            Instantiate(arrow, arrowBox.transform.position, arrowBox.transform.rotation);
            canShoot = false;
            StartCoroutine(ShootArrowCooldown());
            
            playerAnim.SetBool("Moving", false);

            if (isOnGround) {
                playerAnim.Play("Land");
            } else {
                playerAnim.Play("Jump");
            }
        }
        
    }

    private IEnumerator ShootArrowCooldown() {
        yield return new WaitForSeconds(arrowCooldown);
        canShoot = true;
    }
}
