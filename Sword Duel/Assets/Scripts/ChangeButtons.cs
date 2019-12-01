using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject button1;
    public GameObject button2;
    public string scene_name;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TaskOnClick()
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

        GameObject gob = GameObject.Find("2_players");
        gob.SetActive(false);
        gob = GameObject.Find("1_player");
        gob.SetActive(false);

        ChangeLevel bok = button1.transform.GetChild(1).gameObject.GetComponent<ChangeLevel>();
        bok.scene_name = scene_name;
        bok = button2.transform.GetChild(1).gameObject.GetComponent<ChangeLevel>();
        bok.scene_name = scene_name;

    }
}
