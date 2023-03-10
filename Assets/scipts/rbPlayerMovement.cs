using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class rbPlayerMovement : MonoBehaviour
{
    public int curCol;
    // Start is called before the first frame update
    public enum playerState{
        idle,
        run,
        jump
    }
    public int rank = 0;

    private int colors = 0;
    private float r;
    private float g;
    private float b;
    [SerializeField] private ParticleSystem ps;
    public int score = 0;
    public int hits = 0;
    public Rigidbody2D kernals;
    public int ammo = 0;
    private int eat = 0;
    public bool cornE = false;

    public float DT = 0;

    private PolygonCollider2D PC;
    private BoxCollider2D BC;

    Quaternion pivotPoint = new Quaternion (0f,0f,90f,10f);
    public Rigidbody2D projectile;
    public playerState state;
    public int scen = 0;
    useLess useLess;
    MainManager MM;
    public Animator AM;
    public Transform TF;
    private Rigidbody2D rb;

    public int totalPlayers = 0;
    SpriteRenderer sr;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 2.1f;
    private Vector2 move = new Vector2(0f,0f);
    private bool groundedPlayer;
    public bool canJump = false;
    private Vector2 movementInput = Vector2.zero;
    public static rbPlayerMovement Instance;
    [SerializeField] private GameObject pauseMenu;
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
        rb = GetComponent<Rigidbody2D>();
        MM.PlayerN += 1;
        PN = MM.PlayerN;


        BC = gameObject.GetComponent<BoxCollider2D>();
        PC = gameObject.GetComponent<PolygonCollider2D>();
        if(MM.usedColors.Contains(colors)){
            MM.usedColors.Remove(colors);
        }


        colors = Random.Range(1, 9);
        while(MM.usedColors.Contains(colors)){
            colors = Random.Range(1, 9);
        }
        MM.usedColors.Add(colors);
        switch(colors){
            case 1:
                sr.color = MM.red;
                curCol = 1;
                break;
            case 2:
                sr.color = MM.green;
                curCol = 2;
                break;
            case 3:
                sr.color = MM.blue;
                curCol = 3;
                break;
            case 4:
                sr.color = MM.yellow;
                curCol = 4;
                break;
            case 5:
                sr.color = MM.orange;
                curCol = 5;
                break;
            case 6:
                sr.color = MM.purple;
                curCol = 6;
                break;
            case 7:
                sr.color = MM.black;
                curCol = 7;
                break;
            case 8:
                sr.color = MM.white;
                curCol = 8;
                break;
            default:
                Debug.Log("color no work :.(");;
                break;
        }


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

    public void OnMove(InputAction.CallbackContext context) {
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("rbSampleScene")){

        } else {
    movementInput = context.ReadValue<Vector2>();

    state = playerState.run;
    AM.SetBool("run", true);
        }

    }
    public void OnSkip()
    {
        Debug.Log("skip");
        MM.tuttim = 0f;
    }

    public void OnJump(InputAction.CallbackContext context){
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("rbSampleScene")){}
        else if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("rb") && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("SampleScene") ){

        state = playerState.jump;
        AM.SetBool("run", true);
        //transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        Vector2 boing = new Vector2(0, jumpHeight);
        if(canJump){
        rb.AddForce(boing);
        }
        }
    }
    public void OnJoin(InputAction.CallbackContext context){
        if(context.action.triggered){
    if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("rbSampleScene")){
        if(MM.usedColors.Contains(colors)){
            MM.usedColors.Remove(colors);
        }


        colors = Random.Range(1, 9);
        while(MM.usedColors.Contains(colors)){
            colors = Random.Range(1, 9);
        }
        MM.usedColors.Add(colors);
        curCol = colors;
        switch(colors){
            case 1:
                sr.color = MM.red;
                break;
            case 2:
                sr.color = MM.green;
                break;
            case 3:
                sr.color = MM.blue;
                break;
            case 4:
                sr.color = MM.yellow;
                break;
            case 5:
                sr.color = MM.orange;
                break;
            case 6:
                sr.color = MM.purple;
                break;
            case 7:
                sr.color = MM.black;
                break;
            case 8:
                sr.color = MM.white;
                break;
            default:
                Debug.Log("color no work :.(");
                break;
        }
        //sr.color = MM.blue;

        } else {

        }
        }

    }
    public void OnAttack(InputAction.CallbackContext context){
        if (context.action.triggered)
        {
        if(MM.currentDevil == PN){

            Rigidbody2D clone;

            clone = Instantiate(projectile, new Vector2 (transform.position.x, transform.position.y) , projectile.transform.rotation);

            // Give the cloned object an initial velocity along the current
            // object's Z axis

            if(sr.flipX == true){
            clone.transform.Rotate (0f, 0f, 0f);
            clone.transform.position += Vector3.right/2.8f;

            } else {
                clone.transform.Rotate (0f, 0f, 180f);
                clone.transform.position += Vector3.left/2.8f;
            }
             clone.velocity = transform.TransformDirection(Vector3.forward * 100);

        } else if (cornE){
            eat += 1;
            if(eat >= 10){
                ammo += 20;
                cornE = false;
                AM.SetBool("CornEat", false);
                eat = 0;
            }
        } else if (ammo > 0){
            ammo -= 1;
            Debug.Log("spit corn");

            Rigidbody2D clone;

            clone = Instantiate(kernals, new Vector2 (transform.position.x, transform.position.y) , kernals.transform.rotation);

            // Give the cloned object an initial velocity along the current
            // object's Z axis

            if(sr.flipX == true){
            clone.transform.Rotate (0f, 0f, 0f);
            clone.transform.position += Vector3.right/2.8f;

            } else {
                clone.transform.Rotate (0f, 0f, 180f);
                clone.transform.position += Vector3.left/2.8f;
            }
             clone.velocity = transform.TransformDirection(Vector3.forward * 100);
        }
        }
    }
    public void OnStart(InputAction.CallbackContext context){
        if (context.action.triggered)
        {
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("rbSampleScene"))
        {

        if (MM.usedScen.Count < 4)
        {scen = Random.Range(2, 6);

            while (MM.usedScen.Contains(scen))
                {
                    scen = Random.Range(2, 6);
                }

        MM.usedScen.Add(scen);
        //Set scene here for testing specific scene
        //scen = 5;
        SceneManager.LoadScene(scen);
        }
        }
        }
        }


    void Update()
    {

        totalPlayers = MM.PlayerN;
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("rbSampleScene")){
            transform.position = new Vector2(5.0f/(MM.PlayerN + 2) * PN -3f, -0.5f);

           // TF.position.x = 200 * useLess.PlayerNumber;
           // transform.position = new Vector3(transform.position.x, transform.position.y + 0.1, transform.position.z)
        } else {
       // if (/*groundedPlayer && */playerVelocity.y < 0)
       // {
       //     playerVelocity.y = 0f;
        //}

        if(MM.currentDevil == PN){
            AM.SetBool("devil", true);
            gameObject.tag = "Devil";
            // your the devil stuff
            DT += Time.deltaTime;
            Vector2 move = new Vector2(movementInput. x,movementInput.y * 3);
            rb.AddForce(move * playerSpeed * Time.deltaTime);
        } else if (cornE){

        } else {
            gameObject.tag = "Player";
            AM.SetBool("devil", false);
            Vector2 move = new Vector2(movementInput.x,0);
        rb.AddForce(move * playerSpeed * Time.deltaTime);




        }


       // if (move != Vector2.zero)
       // {
       //     gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..

        if(movementInput.x == 0 && movementInput.y == 0)
        {
            state = playerState.idle;
            AM.SetBool("run", false);
        }
        if(movementInput.x > 0){
            sr.flipX = true;
        } else if (movementInput.x < 0){
            sr.flipX = false;
        }


    }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
    Collider2D col = collision.gameObject.GetComponent<Collider2D>();
    if(collision.gameObject.tag == "fireBall" ){
        MM.currentDevil = PN;
        ps.Play();
    }
    if(collision.gameObject.tag == "ground" && BC.IsTouching(col)){
        canJump = true;
       // Debug.Log(collision.gameObject.transform.position.y);
       // Debug.Log(collision.gameObject.transform.position.y > TF.position.y);
    } else if (collision.gameObject.tag == "corn" ){
        cornE = true;
        AM.SetBool("CornEat", true);
        Destroy(collision.gameObject);

    } else if (collision.gameObject.tag == "kernal"){
        hits += 1;

    }
    }
    void OnCollisionExit2D(Collision2D col){
    if(col.gameObject.tag == "ground"){
        canJump = false;
    } else {
       // canJump = true;
    }
    }
    void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.tag = "Player";

        ammo = 0;
        //TF.position = new Vector2(-2.5f + PN,0);
        TF.position = new Vector2(0,0);
        cornE = false;
        AM.SetBool("CornEat", false);
        AM.SetBool("run", false);
        eat = 0;
        hits = 0;
        DT = 0;
        pauseMenu = GameObject.Find("Menu");
    }

    // called third


    // called when the game is terminated
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
