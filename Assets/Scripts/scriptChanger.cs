using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptChanger : MonoBehaviour
{
    [SerializeField] private GameObject player;
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
            player.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            rb.gravityScale = 1.75f;
            rb.mass = 0.5f;
            anim.SetBool("devil", false);
            ianPlayerScript.enabled = true;
            grantPlayerScript.enabled = false;
        }
        //else if (SceneManager.GetActiveScene.name == "brennon"){}
        else
        {
            rb.freezeRotation = true;
            transform.rotation = new Vector3(0, 0, 0);
            player.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            rb.mass = 1.0f;
            rb.gravityScale = 2.0f;
            ianPlayerScript.enabled = false;
            grantPlayerScript.enabled = true;
        }
    }
}
