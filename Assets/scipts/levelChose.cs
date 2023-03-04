using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelChose : MonoBehaviour
{
    public Transform tf;
    public MainManager MM;
    public int thisWorld = 0;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        MM = GameObject.Find("MainManager").GetComponent<MainManager>();
        MM.world += 1;
        thisWorld = MM.world;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MM.curWorld == thisWorld){
            tf.position = new Vector2(0,0);
        }
    }
}
