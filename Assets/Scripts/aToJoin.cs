using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class aToJoin : MonoBehaviour
{
    MainManager MM;
    public TMP_Text A1;
    public TMP_Text A2;
    public TMP_Text A3;
    public TMP_Text A4;
    public TMP_Text A5;
    public TMP_Text A6;
    public TMP_Text A7;
    public TMP_Text A8;
    // Start is called before the first frame update
    void Start()
    {
        MM = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MM.PlayerN >= 1){
            A1.text = "Player 1";
        }
        if(MM.PlayerN >= 2){
            A2.text = "Player 2";
        }
        if(MM.PlayerN >= 3){
            A3.text = "Player 3";
        }
        if(MM.PlayerN >= 4){
            A4.text = "Player 4";
        }
        if(MM.PlayerN >= 7){
            A7.text = "Player 7";
        }
        if(MM.PlayerN >= 5){
            A5.text = "Player 5";
        }
        if(MM.PlayerN >= 6){
            A6.text = "Player 6";
        }
        if(MM.PlayerN >= 8){
            A8.text = "Player 8";
        }
    }
}
