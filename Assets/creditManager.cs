using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class creditManager : MonoBehaviour
{
    private bool credited = false;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject cantEven;
    [SerializeField] private float time = 15f;
    // Start is called before the first frame update
    public void onSkip()
    {
        time = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            if (!credited)
            {
                credits.SetActive(true);
                cantEven.SetActive(false);
                time -= Time.deltaTime;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
        else
        {
            if (credited)
            {
                SceneManager.LoadScene(0);
            }
            credited = true;
            time = 6.5f;
            credits.SetActive(false);
            cantEven.SetActive(true);
        }
    }
}
