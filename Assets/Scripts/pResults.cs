using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pResults : MonoBehaviour
{
    [SerializeField] private rbPlayerMovement scr;
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Results")
        {
            switch(scr.rank)
            {
                case 1:
                    transform.position = new Vector3(0f, 1.58f, 0f);
                    transform.localScale = new Vector3(0.85f, 0.85f, 2f);
                    rb.velocity = new Vector2(0f, 0f);
                    break;
                case 2:
                    transform.position = new Vector3(-5.55f, -0.31f, 0f);
                    transform.localScale = new Vector3(0.85f, 0.85f, 2f);
                    rb.velocity = new Vector2(0f, 0f);
                    break;
                case 3:
                    transform.position = new Vector3(5.6f, -1.64f, 0f);
                    transform.localScale = new Vector3(0.85f, 0.85f, 2f);
                    rb.velocity = new Vector2(0f, 0f);
                    break;
                case 4:
                    transform.position = new Vector3(-2.65f, -3.25f, 0f);
                    transform.localScale = new Vector3(0.5f, 0.5f, 2f);
                    rb.velocity = new Vector2(0f, 0f);
                    break;
                case 5:
                    transform.position = new Vector3(-1.3f, -3.25f, 0f);
                    transform.localScale = new Vector3(0.5f, 0.5f, 2f);
                    rb.velocity = new Vector2(0f, 0f);
                    break;
                case 6:
                    transform.position = new Vector3(0f, -3.25f, 0f);
                    transform.localScale = new Vector3(0.5f, 0.5f, 2f);
                    rb.velocity = new Vector2(0f, 0f);
                    break;
                case 7:
                    transform.position = new Vector3(1.175f, -3.25f, 0f);
                    transform.localScale = new Vector3(0.5f, 0.5f, 2f);
                    rb.velocity = new Vector2(0f, 0f);
                    break;
                case 8:
                    transform.position = new Vector3(2.3f, -3.25f, 0f);
                    transform.localScale = new Vector3(0.5f, 0.5f, 2f);
                    rb.velocity = new Vector2(0f, 0f);
                    break;
            }
        }
    }
}
