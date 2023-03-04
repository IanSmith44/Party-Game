using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovemet : MonoBehaviour
{
    public enum playerState{
        idle,
        run,
        jump
    }
    private float r;
    private float g;
    private float b;

    private BoxCollider BC;

    Quaternion pivotPoint = new Quaternion (0f,0f,90f,10f);
    public Rigidbody projectile;
    public playerState state;
    useLess useLess;
    MainManager MM;
    public Animator AM;
    public Transform TF;
    private Rigidbody rb;
    public int totalPlayers = 0;
    SpriteRenderer sr;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 2.1f;
    public float gravityValue = -9.81f;
    private CharacterController controller;
    private Vector2 playerVelocity;
    private bool groundedPlayer;
    public bool canJump = false;
    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;
    public static playerMovemet Instance;
    private int PN = 0;
    void Awake(){
        r = Random.value;
    g = Random.value;
    b = Random.value;
    Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        MM = GameObject.Find("MainManager").GetComponent<MainManager>();
        useLess = GameObject.Find("PlayerManager").GetComponent<useLess>();
        sr = GetComponent<SpriteRenderer>();
        AM = GetComponent<Animator>();
        TF = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        useLess.PlayerNumber += 1;
        PN = useLess.PlayerNumber;


        controller = gameObject.GetComponent<CharacterController>();
        BC = gameObject.GetComponent<BoxCollider>();
        if(useLess.colr == 0){
            sr.color = new Color(r, g, b);
            useLess.colr += 1;
        } else if(useLess.colr == 1){
            sr.color = new Color(1f, 0.30196078f, 0.30196078f);
            useLess.colr += 1;
        } else if(useLess.colr == 2){
            sr.color = new Color(0.3f, 1f, 0.3f);
            useLess.colr += 1;
        } else if(useLess.colr == 3){
            sr.color = new Color(0.3f, 0.30196078f, 1f);
            useLess.colr += 1;
        } else {
            sr.color = new Color(r, g, b);
        }
        

    }

    public void OnMove(InputAction.CallbackContext context) {
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("SampleScene")){
           
        } else {
    movementInput = context.ReadValue<Vector2>();
    state = playerState.run;
    AM.SetBool("run", true);
        }
    
    }

    public void OnJump(InputAction.CallbackContext context){
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("SampleScene")){}
        else{
        jumped = context.action.triggered;
        state = playerState.jump;
        AM.SetBool("run", true);
        }
    }
    public void OnJoin(InputAction.CallbackContext context){
    if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("SampleScene")){
            r = Random.value;
        g = Random.value;
        b = Random.value;
            sr.color = new Color(r, g, b);
        } 
    
    }
    public void OnAttack(InputAction.CallbackContext context){
        if(MM.currentDevil == PN){
            Debug.Log("attack");
            Rigidbody clone;
            
            clone = Instantiate(projectile, transform.position += Vector3.up/5, projectile.transform.rotation);

            // Give the cloned object an initial velocity along the current
            // object's Z axis

            if(sr.flipX == true){
            clone.transform.Rotate (0f, 0f, 0f);
            clone.transform.position += Vector3.right/3;
            
            } else {
                clone.transform.Rotate (0f, 0f, 180f);
                clone.transform.position += Vector3.left/3;
            }
             clone.velocity = transform.TransformDirection(Vector3.forward * 100);
            
        }
    }
    public void OnStart(InputAction.CallbackContext context){
        
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("SampleScene"))
        {
        SceneManager.LoadScene(1);
        } 
    }

    void Update()
    {
        totalPlayers = useLess.PlayerNumber;
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("SampleScene")){
            transform.position = new Vector3(5.0f/(useLess.PlayerNumber + 2) * PN -3f, -0.5f, 0.1f);
            
           // TF.position.x = 200 * useLess.PlayerNumber;
        } else {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if(MM.currentDevil == PN){
            AM.SetBool("devil", true);
            gameObject.tag = "Devil";
            // your the devil stuff
           // Debug.Log("player; " + PN + " is the devil");
           Vector2 move = new Vector2(movementInput.x,  movementInput.y);
            controller.Move(move * Time.deltaTime * playerSpeed);
        } else {
            gameObject.tag = "Player";
            AM.SetBool("devil", false);
        
        
        Vector2 move = new Vector2(movementInput.x, 0);
        
        controller.Move(move * Time.deltaTime * playerSpeed);
        }

       // if (move != Vector2.zero)
       // {
       //     gameObject.transform.forward = move;
        //}
        
        // Changes the height position of the player..
        if (jumped && groundedPlayer && MM.currentDevil != PN)
        {

            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            canJump = false;
        }
        if(movementInput.x == 0 && playerVelocity.y == 0)
        {
            state = playerState.idle;
            AM.SetBool("run", false);
        }
        if(movementInput.x > 0){
            sr.flipX = true;
        } else if (movementInput.x < 0){
            sr.flipX = false;
        }
        if(MM.currentDevil != PN){
        playerVelocity.y += gravityValue * Time.deltaTime;
        }
        controller.Move(playerVelocity * Time.deltaTime);
       // rb.AddForce(playerVelocity * Time.deltaTime);
    }
    }
     void OnCollisionStay(Collision collision)
  {
    if(collision.gameObject.tag == "fireBall" ){
        MM.currentDevil = PN;
    }
  }
}
    
  

// gameObject.tag = "UnitSelected";

