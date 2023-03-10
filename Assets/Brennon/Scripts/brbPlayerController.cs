using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class brbPlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem psr;
    [SerializeField] private ParticleSystem psg;
    [SerializeField] private ParticleSystem psb;
    [SerializeField] private ParticleSystem psy;
    [SerializeField] private ParticleSystem pso;
    [SerializeField] private ParticleSystem psp;
    [SerializeField] private ParticleSystem psbl;
    [SerializeField] private ParticleSystem pswh;
    MainManager MM;
    public Rigidbody2D rb;

    public Animator animator;

    public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping,
        SprintJumping
    }
    [SerializeField] private rbPlayerMovement grantScrip;
    public PlayerState State;
    public Collider2D leftTrigger;
    public Collider2D rightTrigger;
    private GameObject collidingWith;
    brbPlayerController PC;
    [SerializeField] private float playerSpeed = 1.0f;
    [SerializeField] private float jumpHeight = 0.0f;
    [SerializeField] private bool grounded;
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    private bool sprintin = false;
    private bool attacked = false;
    private int stockCounter = 3;
    public float hitMultiplier = 1.0f;
    private SpriteRenderer sr;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        MM = GameObject.Find("MainManager").GetComponent<MainManager>();
        sr = GetComponent<SpriteRenderer>();
    }
        /*public void OnPause(InputAction.CallbackContext context)
        {
            if (context.action.triggered)
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                if (Time.timeScale == 1)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
        }*/

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

    public void onHit()
    {
        switch(grantScrip.curCol) {
            case 1:
                psr.Play();
                break;
            case 2:
                psg.Play();
                break;
            case 3:
                psb.Play();
                break;
            case 4:
                psy.Play();
                break;
            case 5:
                pso.Play();
                break;
            case 6:
                psp.Play();
                break;
            case 7:
                psbl.Play();
                break;
            case 8:
                pswh.Play();
                break;
        }
    }
    public void OnSkip()
    {
        MM.tuttim = 0f;
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        sprintin = context.action.triggered;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        attacked = context.action.triggered;
        //Debug.Log(collidingWith);
        if (collidingWith != null)
        {
            //Debug.Log(hitMultiplier);
            float xForce = 50f;
            float yForce = 50f;
            if (!sr.flipX)
            {
                xForce = -xForce;
            }
            PC = collidingWith.GetComponent<brbPlayerController>();
            collidingWith.GetComponent<Rigidbody2D>().AddForce(new Vector3(xForce, yForce, 0) * PC.hitMultiplier);
            collidingWith.GetComponent<brbPlayerController>().onHit();
            PC.hitMultiplier += 0.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D HitCheck)
    {
        if (HitCheck.gameObject.tag == "Player")
        {
            collidingWith = HitCheck.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collidingWith = null;
    }

    void Update()
    {
        rightTrigger.enabled = sr.flipX;
        leftTrigger.enabled = !sr.flipX;

        if (!grounded && sprintin)
        {
            State = PlayerState.SprintJumping;
            animator.SetBool("run", true);
        }
        else if (!grounded)
        {
            State = PlayerState.Jumping;
            animator.SetBool("run", true);
        }
        else if (Mathf.Abs(movementInput.x) < 0.1f)
        {
            State = PlayerState.Idle;
            animator.SetBool("run", false);
        }
        else if (movementInput.x != 0 && grounded && !sprintin)
        {
            State = PlayerState.Walking;
            animator.SetBool("run", true);
        }
        else if (movementInput.x != 0 && grounded && sprintin)
        {
            State = PlayerState.Running;
            animator.SetBool("run", true);
        }
        Vector3 move = new Vector2(movementInput.x, 0);
        float speedMultiplier = sprintin ? 4 : 2;
        rb.AddForce(move * Time.deltaTime * playerSpeed * speedMultiplier);
        if (jumped && grounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
            grounded = false;
        }

        if (transform.position.x >= 30 || transform.position.x <= -30 || transform.position.y >= 30 || transform.position.y <= -30)
        {
            if(tag == "Player")
            {
                transform.position = new Vector3(0, 0, 0);
                stockCounter = stockCounter - 1;
                hitMultiplier = 1f;
            }
        }

        if (stockCounter == 0)
        {
            transform.position = new Vector2(5500,0);
            gameObject.tag = "ground";
        }

        sr.flipX = movementInput.x < 0 ? false : (movementInput.x > 0 ? true : sr.flipX);
    }

    void FixedUpdate()
    {
        if (movementInput == Vector2.zero)
        {
            rb.velocity = new Vector2(rb.velocity.x / 1.1f, rb.velocity.y);
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" /*|| collision.gameObject.tag == "Player"*/)
        {
            grounded = true;
        }

    }
    void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        animator.SetBool("run", false);
        gameObject.tag = "Player";
        grantScrip.score += stockCounter;

        transform.position = new Vector2(0,0);

        //pauseMenu = GameObject.Find("Menu");

    }

    // called third


    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}