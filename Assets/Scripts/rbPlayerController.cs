using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class rbPlayerController : MonoBehaviour
{
    MainManager MM;
    [SerializeField] private GameObject pauseMenu;
    private bool spin;
    [SerializeField] private rbPlayerMovement grantPlayerScript;
    [SerializeField] private Transform enemy;
    [SerializeField] private float camDistance = 7.5f;
    [SerializeField] private Camera cam;
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
    public PlayerState State;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private bool grounded;
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    private bool sprintin = false;
    private SpriteRenderer sr;

    private void Start()
    {
        MM = GameObject.Find("MainManager").GetComponent<MainManager>();
        /*
        enemy = GameObject.Find("Fire").transform;
        cam = FindObjectOfType<Camera>();;
        sr = GetComponent<SpriteRenderer>();
        */
    }
        public void OnPause(InputAction.CallbackContext context)
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
        }
        public void OnSpin(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            spin = !spin;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumped = context.action.triggered;
    }

    public void OnSkip()
    {
        MM.tuttim = 0f;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        sprintin = context.action.triggered;
    }

    void Update()
    {
        if (spin)
        {
            rb.freezeRotation = false;
        }
        else
        {
            rb.freezeRotation = true;
        }
        enemy = GameObject.Find("Fire").transform;
        cam = FindObjectOfType<Camera>();
        sr = GetComponent<SpriteRenderer>();
        if (transform.position.x < enemy.position.x)
        {
            transform.position = new Vector2(5500,0);
            gameObject.tag = "ground";
            //Destroy(gameObject);
        }
        if (cam.transform.position.x - transform.position.x > camDistance && gameObject.tag != "ground")
        {
            transform.position = new Vector3(cam.transform.position.x - camDistance + 0.05f, transform.position.y, transform.position.z);
        }
        if (cam.transform.position.x - transform.position.x < -1 * camDistance && gameObject.tag != "ground")
        {
            transform.position = new Vector3(cam.transform.position.x + camDistance - 0.05f, transform.position.y, transform.position.z);
        }
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
            rb.velocity = new Vector2(rb.velocity.x,jumpHeight);
            //rb.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
            grounded = false;
            //Debug.Log("Jumped!");
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
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            //Debug.Log("Grounded!");
        }
        if (collision.gameObject.tag == "Die")
        {
            grantPlayerScript.score -= 1;
            transform.position = new Vector2(5500,0);
            gameObject.tag = "ground";
            Debug.Log("die?");
        }
        if (collision.gameObject.tag == "Jump Pad")
        {
            rb.velocity = new Vector2(rb.velocity.x, 20);
        }
    }
    void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        pauseMenu = GameObject.Find("Menu");
        //TF.position = new Vector2(-2.5f + PN,0);
        transform.position = new Vector2(0,0);
        gameObject.tag = "Player";
    }

    // called third


    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
