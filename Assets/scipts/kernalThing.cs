using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kernalThing : MonoBehaviour
{
    private float DT = 0f;
    private Rigidbody2D rb;
    Transform tf;
    public bool goRight = true;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        if (tf.rotation.eulerAngles.z == 0)
{
        rb.AddForce(Vector3.right*1000);
        } else{
            rb.AddForce(Vector3.left*1000);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(tf.position.y < -5){
            Destroy(gameObject);
        }
        DT += Time.deltaTime;
        if (DT > 4){
            Destroy(gameObject);
        }
        
        
    }
    void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
    }
}

