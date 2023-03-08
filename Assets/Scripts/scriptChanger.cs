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
    [SerializeField] private brbPlayerController brennonPlayerScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "rb")
        {
            rb.drag = 1f;
            player.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            rb.gravityScale = 1.75f;
            rb.mass = 0.5f;
            anim.SetBool("devil", false);
            ianPlayerScript.enabled = true;
            grantPlayerScript.enabled = false;
            brennonPlayerScript.enabled = false;
        }
        else if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            rb.mass = 1f;
            rb.drag = 1.25f;
            rb.gravityScale = 3.75f;
            player.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            brennonPlayerScript.enabled = true;
            ianPlayerScript.enabled = false;
            grantPlayerScript.enabled = false;
            anim.SetBool("devil", false);
        }
        else
        {
            rb.drag = 1f;
            rb.freezeRotation = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            player.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            rb.mass = 1.0f;
            rb.gravityScale = 2.0f;
            ianPlayerScript.enabled = false;
            grantPlayerScript.enabled = true;
            brennonPlayerScript.enabled = false;
        }
    }
}
