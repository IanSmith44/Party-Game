using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CornGame : MonoBehaviour
{
    public MainManager MM;
    public Rigidbody2D corn;
    private float bob = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("CornEat")){
            
        }
        MM = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene () == SceneManager.GetSceneByName ("CornEat")){
            bob += Time.deltaTime;
            if(bob >= 15/MM.PlayerN){
            Rigidbody2D clone;
            
            clone = Instantiate(corn, new Vector2 (Random.Range(-9, 10), Random.Range(-5,6)) , corn.transform.rotation);
            bob = 0;
            }

            // Give the cloned object an initial velocity along the current
            // object's Z axis

        
        }
    }
}
