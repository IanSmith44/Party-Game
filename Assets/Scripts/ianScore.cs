using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ianScore : MonoBehaviour
{
    private MainManager MM;
    private bool _1st = false;
    private bool _2nd = false;
    private bool _3rd = false;
    private bool _4th = false;
    private bool _5th = false;
    private bool _6th = false;
    private bool _7th = false;
    private bool _8th = false;
    // Start is called before the first frame update
    void Start()
    {
        MM = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] targets = null;
        targets = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(targets.Length);
        if (targets.Length == 0)
        {
            Debug.Log("No players");
            MM.time = 0;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!_1st)
            {
                collision.gameObject.GetComponent<rbPlayerMovement>().score += 4;
                _1st = true;
            }
            else if (!_2nd)
            {
                collision.gameObject.GetComponent<rbPlayerMovement>().score += 3;
                _2nd = true;
            }
            else if (!_3rd)
            {
                collision.gameObject.GetComponent<rbPlayerMovement>().score += 2;
                _3rd = true;
            }
            else if (!_4th)
            {
                collision.gameObject.GetComponent<rbPlayerMovement>().score += 1;
                _4th = true;
            }
            else if (!_5th)
            {
                collision.gameObject.GetComponent<rbPlayerMovement>().score += 0;
                _5th = true;
            }
            else if (!_6th)
            {
                collision.gameObject.GetComponent<rbPlayerMovement>().score -= 1;
                _6th = true;
            }
            else if (!_7th)
            {
                collision.gameObject.GetComponent<rbPlayerMovement>().score -= 2;
                _7th = true;
            }
            else if (!_8th)
            {
                collision.gameObject.GetComponent<rbPlayerMovement>().score -= 3;
                _8th = true;
            }
            collision.gameObject.transform.position = new Vector2(5500,0);
            collision.gameObject.tag = "ground";
        }
    }
}
