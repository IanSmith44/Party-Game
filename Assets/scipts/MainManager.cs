using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    public List<int> usedColors = new List<int>();
    public Color red = new Color(255f,0f,0f);
    public Color green = new Color(0f,255f,0f);
    public Color blue = new Color(0f,0f,255f);
    public Color yellow = new Color(255f,255f,0f);
    public Color orange = new Color(255f,166f,0f);
    public Color purple = new Color(128f,0f,128f);
    public Color black = new Color(0f,0f,0f);
    public Color white = new Color(255f,255f,255f);


    private bool go = true;
    public List<int> usedScen = new List<int>();
    public float tuttim = 15f;
    [SerializeField] private GameObject tutpan;
    public bool tut = true;
    public string curScene = "";
    private int num = 0;
    private float temp = 0f;
    private GameObject[] arr = {};
    private float[] score = {};
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;
    public GameObject player6;
    public GameObject player7;
    public GameObject player8;
    public TMP_Text TL;
    public float time = 120;
    public float currentDevil = 0f;
    public int PlayerN = 0;
    public useLess useLess;
    private int sce = 0;
    public static MainManager Instance;
    public int world = 0;
    public int curWorld = 0;
    private rbPlayerMovement PM;

    private void Awake()
    {
        // start of new code
    if (Instance != null)
    {
        Destroy(gameObject);
        return;
    }
    // end of new code

    Instance = this;
    DontDestroyOnLoad(gameObject);
    }
    void Start()
    {

        //Color[] colorlist = {0f, 0f, 0f, 1f,new Color(255f, 255f, 255f, 1f),new Color(255f, 255f, 0f), new Color(0f, 0f, 255f), new Color(0f, 255f, 0f),new Color(255f, 0f, 0f),new Color(255f, 165f, 0f),new Color(128f, 0f, 128f)};
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    void Update(){
        go = true;
        if (tutpan != null)
        {
            if (tutpan.activeSelf == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        if (tut && tutpan != null)
        {
            tutpan.SetActive(true);
        }
        else
        {
            if (tutpan != null)
            {
                tutpan.SetActive(false);
            }
        }
        if (tuttim <= 0)
        {
            tut = false;
        }
        else
        {
            tuttim -= Time.unscaledDeltaTime;
        }
        time -= Time.deltaTime;
        if(time <= 0){
            if (SceneManager.GetActiveScene().name == "Results")
            {
                SceneManager.LoadScene(0);
            }
        else if(usedScen.Count >= 4)
            {
                SceneManager.LoadScene(6);
            }
            if (usedScen.Count < 4)
            {sce = Random.Range(2, 6);
            while (usedScen.Contains(sce))
                {
                    sce = Random.Range(2, 6);
                }

            usedScen.Add(sce);
            SceneManager.LoadScene(sce);
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Results"))
            {
                time = 30f;
            }
            else if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("rb"))
            {
                time = 120f;
            }
            else
            {
                time = 300f;
            }
        }
        TL.text = "Time Left: " + Mathf.Round(time);



        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("Play")){
            if(currentDevil == 0f){
            currentDevil = Random.Range(1, PlayerN + 1);
            if(curWorld == 0){
            curWorld = Random.Range(1, world + 1);
            }
            }

        } else if (SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("CornEat")){
            if(curWorld == 0){
            curWorld = Random.Range(1, world);
            }
        }
        curScene = SceneManager.GetActiveScene().name;
    }
    void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(go){
            go = false;
        tut = true;
        tuttim = 15f;
        tutpan = GameObject.Find("TutorialPanel");

        if(!player1){
        player1 = GameObject.Find("Player(Clone)");
        player1.name = "P1";
        player2 = GameObject.Find("Player(Clone)");
        if (player2 != null)
        {
            player2.name = "P2";
        }
        player3 = GameObject.Find("Player(Clone)");
        if (player3 != null)
        {
            player3.name = "P3";
        }
        player4 = GameObject.Find("Player(Clone)");
        if (player4 != null)
        {
            player4.name = "P4";
        }
        player5 = GameObject.Find("Player(Clone)");
        if (player5 != null)
        {
            player5.name = "P5";
        }
        player6 = GameObject.Find("Player(Clone)");
        if (player6 != null)
        {
            player6.name = "P6";
        }
        player7 = GameObject.Find("Player(Clone)");
        if (player7 != null)
        {
            player7.name = "P7";
        }
        player8 = GameObject.Find("Player(Clone)");
        if (player8 != null)
        {
            player8.name = "P8";
        }
        }
        float[] score = {400f,400f,400f,400f,400f,400f,400f,400f};
        GameObject[] arr = {player1,player2,player3,player4,player5,player6,player7,player8};
        curWorld = 0;
        world = 0;
        currentDevil = 0f;
        Debug.Log("running time");
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Results"))
            {
                time = 30f;
            }
            else if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("rb"))
            {
                time = 120f;
            }
            else
            {
                time = 300f;
            }
        Debug.Log("ran time");
        if(curScene == "Play" || curScene == "CornEat"){
        Debug.Log(curScene);
        for(int i = 0; i < PlayerN; i++ ){
            //Debug.Log("player " + i);
            PM = arr[i].GetComponent<rbPlayerMovement>();
           // Debug.Log(PM.DT);
           // Debug.Log(PM.hits);
            score[i] = PM.DT + PM.hits;
           // Debug.Log(score[i]);
        }

        num = 5;
        for(int i = 0; i < 4; i++){
            num--;
            temp = Mathf.Min(score[1], score[2],score[3],score[4],score[5],score[6],score[7],score[0]);
            //Debug.Log(temp);
            for(int j = 0; j < PlayerN; j++){
                if(arr[i] != null){
                PM = arr[j].GetComponent<rbPlayerMovement>();
                if(temp == PM.DT + PM.hits && temp != 400f){
                    Debug.Log(num);
                    Debug.Log(PM.score);
                    PM.score += num;
                    Debug.Log(PM.score);
                    score[j] = 400f;
                    Debug.Log("im so confused");
                }
            }
            }

        }

    }

    if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Results")){
        for(int i = 0; i < 8; i++){
         score[i] = 0;
    }
         for(int i = 0; i < PlayerN; i++ ){
            //Debug.Log("player " + i);
            PM = arr[i].GetComponent<rbPlayerMovement>();
           // Debug.Log(PM.DT);
           // Debug.Log(PM.hits);
            score[i] = PM.score;
            //Debug.Log(score[i]);
        }
num = 0;

        for(int i = 0; i < PlayerN; i++){
            num++;
            temp = Mathf.Max(score[1], score[2],score[3],score[4],score[5],score[6],score[7],score[0]);
            //Debug.Log(temp);
            for(int j = 0; j < PlayerN; j++){
                if(arr[i] != null){
                PM = arr[j].GetComponent<rbPlayerMovement>();
                if(temp == PM.score && temp != 0){

                    PM.rank = i + 1;

                    score[j] = 0;
                }
            }
            }
        }
        Debug.Log(num);

    }
        }
    }

    // called third
    // called when the game is terminated
    void OnDisable()
    {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
