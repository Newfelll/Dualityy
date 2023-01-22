using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : MonoBehaviour
{
    [Header("Components")]
   
    private Rigidbody2D playerRb;
    private CircleCollider2D boxCollider;
    public GameObject background;
    private BoxCollider2D back;
    private AudioSource audioSource;
    public Animator SquashAndStretchAnimator;
    public SpriteRenderer playerSprite;
    public PlatformEffector2D oneWayPlatform;
    public ParticleSystem speedLines;
    public GameObject[] hearts;
    public GameObject powerUpSprite;
    RaycastHit2D raycastHit2d;
    [SerializeField] private LayerMask ground;
   


    

    [Header("Player properties")]

    [SerializeField] private bool isUp = true;
    [SerializeField] private bool Jump = false;
    [SerializeField] private float divider = 3;
    [SerializeField] private AudioClip transition;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float jumpy = 7f;
    [SerializeField] private float raycastDistance = 0.5f;
    [SerializeField] private bool isFalan=true;
    [SerializeField] private int playerHealth = 3;
    [SerializeField] private float particleLength = 1;
    [SerializeField] private float enemyProximity = 0.80f;
    [SerializeField] private bool invincibilityPower = false;
    [SerializeField] private float downForce = 10f;
    [SerializeField] private bool isAir=false;
    [SerializeField] private bool Dash = false;
    private Vector4 krem = new Vector4(1f, 0.8980392f, 0.8f, 1f);
    private Vector4 siyah = new Vector4(0.1294118f, 0.1254902f, 0.1254902f, 1f);







    Touch touch;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();   
        boxCollider = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        back = background.GetComponent<BoxCollider2D>();


        

        Color krem = new Color(1f, 0.8980392f, 0.8f, 1f);
        Color siyah = new Color(0.1294118f, 0.1254902f, 0.1254902f, 0.3f);

    }

    // Update is called once per frame
    void Update()
    {
        

        if (GameManager.isPlaying)
        {
            if ((Input.GetMouseButton(0) || Input.touchCount > 0) && IsGrounded())
            {

                MoveLeft.SpeedUp();
                speedLines.enableEmission = true;
                speedLines.Play();

                
                


            }

            if (Input.GetMouseButtonUp(0) && IsGrounded())
            {

                speedLines.enableEmission = false;
                back.enabled = false;

                if (oneWayPlatform.rotationalOffset == 180)
                {
                    oneWayPlatform.rotationalOffset = 0;
                }
                else oneWayPlatform.rotationalOffset = 180;

                Jump = true;



            }

            if (Input.GetMouseButtonUp(0) && isAir)
            {
                Dash = true;
            }

            


            if (touch.phase == TouchPhase.Ended&& GameManager.isPlaying)
            {
                
                speedLines.enableEmission = false;
                back.enabled = false;

                if (oneWayPlatform.rotationalOffset == 180)                                      
                {
                    oneWayPlatform.rotationalOffset = 0;
                }
                else oneWayPlatform.rotationalOffset = 180;

                Jump = true;

            }else if (Input.touchCount > 0 && touch.phase == TouchPhase.Ended &&  isAir)
            {
                Dash = true;
            }


            
         

            


            if (transform.position.y < 0 && isFalan)
            {


                ChangeGravity();
                playerSprite.color = siyah;
                speedLines.startColor = siyah;
                speedLines.startSpeed = -5;




                isUp = false;



            }
            else if (transform.position.y > 0 && !isFalan)
            {

                ChangeGravity();
                playerSprite.color = krem; isUp = true;
                speedLines.startColor = krem;
                speedLines.startSpeed = 5;
            }

        }


    }
   


    


    private void FixedUpdate()
    {
        if (Jump)
        {
            DoJump();
            Jump = false;
        }

        if (Dash)
        {
            DoDownDash();
        }
    }




    void DoDownDash()
    {
        playerRb.AddForce(-transform.up * downForce, ForceMode2D.Impulse);

        isAir = false;
        Dash = false;

    }


    void DoJump()
    {   
        SquashAndStretchAnimator.SetTrigger("Jump");

        isAir = true;

        if (isUp)
        {
            
         
            audioSource.Play();
            playerRb.velocity = Vector2.up * jumpVelocity;
        }
        else {  audioSource.Play(); ; playerRb.velocity = Vector2.down * jumpVelocity;  }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            
            
          

            if (!invincibilityPower)
             {
                hearts[playerHealth - 1].SetActive(false);
                playerHealth -= 1;
                transform.localScale = new Vector3(transform.localScale.x / 1.5f, transform.localScale.y / 1.5f, transform.localScale.z / 1.5f);
                jumpVelocity *= 1.1f; 
            }

              if (playerHealth == 0) { FindObjectOfType<GameManager>().GameOver(); }
            


        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBasic")) 
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            
            if (Mathf.Abs(collision.transform.position.y - transform.position.y) > enemyProximity)
            {
                StartCoroutine(PowerUp(5));
                SquashAndStretchAnimator.SetTrigger("Jump");
                if (transform.position.y > 0)
                { 

                    playerRb.AddForce(Vector2.up * jumpy, ForceMode2D.Force);
                }
                else { playerRb.AddForce(Vector2.down * jumpy, ForceMode2D.Force); }
            }
            else
            {
                enemyProximity /= 1.2f; 
                
                if (!invincibilityPower)
                {
                    hearts[playerHealth - 1].SetActive(false);
                    playerHealth -= 1;
                    transform.localScale = new Vector3(transform.localScale.x / 1.5f, transform.localScale.y / 1.5f, transform.localScale.z / 1.5f); 
                    jumpVelocity *= 1.1f; 
                }
                if (playerHealth == 0) { FindObjectOfType<GameManager>().GameOver(); }
            }
           

        }

        if (collision.gameObject.CompareTag("Respawn"))
        {


                isAir = false;
                SquashAndStretchAnimator.SetTrigger("Landing");
            
           
        }
        
    }
    private bool IsGrounded()
    {
        if (isUp)
        {
           
            raycastHit2d = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, raycastDistance, ground);
            
        }
        else { raycastHit2d = Physics2D.Raycast(boxCollider.bounds.center, Vector2.up, raycastDistance, ground); }

        


        return raycastHit2d.collider != null;
    }
    






    void ChangeGravity()
    {
        if (transform.rotation.z == 0)                                                                                              
        {
            back.enabled = true;
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else { back.enabled = true; transform.eulerAngles = new Vector3(0, 0, 0); }
        
        isFalan = !isFalan;
        playerRb.velocity = playerRb.velocity / divider;
        playerRb.gravityScale *= -1;
    }


    IEnumerator PowerUp(float duration)
    {
        
        powerUpSprite.SetActive(true);
        invincibilityPower = true;
        yield return new WaitForSeconds(duration);
        powerUpSprite.SetActive(false);
        invincibilityPower = false;

        
    }

}
