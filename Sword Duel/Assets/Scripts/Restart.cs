using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Restart : MonoBehaviour
{

    float counter = 0f;
    public float death_Screen_time = 5f;
    bool dead = false;
    bool player = false; // false player 1, true 2
    public GameObject canvas;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !dead)
        {
            dead = true;
            if (collision.gameObject.name == "Player1")
                player = false;
            else
                player = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
            return;

        canvas.SetActive(true);
        //write who won
        if (player)
            text.text = "Blue Player Wins!";
        else
            text.text = "Red Player Wins!";

        if (counter < death_Screen_time)
        {
            counter += Time.deltaTime;
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        canvas.SetActive(false);
    }
}
