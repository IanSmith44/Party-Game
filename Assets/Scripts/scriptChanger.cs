using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptChanger : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private rbPlayerController ianPlayerScript;
    [SerializeField] private rbPlayerMovement grantPlayerScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "rb")
        {
            rb.gravityScale = 1.75f;
            rb.mass = 0.5f;
            anim.SetBool("devil", false);
            ianPlayerScript.enabled = true;
            grantPlayerScript.enabled = false;
        }
        //else if (SceneManager.GetActiveScene.name == "brennon"){}
        else
        {
            rb.mass = 1.0f;
            rb.gravityScale = 2.0f;
            ianPlayerScript.enabled = false;
            grantPlayerScript.enabled = true;
        }
    }
}
