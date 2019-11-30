using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    float counter = 0f;
    public float death_Screen_time = 5f;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            dead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
            return;

        if (counter < death_Screen_time)
        {
            counter += Time.deltaTime;
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
