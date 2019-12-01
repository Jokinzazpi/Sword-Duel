using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{

    public float initial_timescale = 0.5f;
    public float max_timescale = 2;
    public float timescale_rate = 0.1f;
    [HideInInspector]
    public string scene_name;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void TaskOnClick()
    {
        SceneManager.LoadScene(scene_name);
        PlayerPrefs.SetFloat("Initial_timescale", initial_timescale);
        PlayerPrefs.SetFloat("Max_timescale",max_timescale);
        PlayerPrefs.SetFloat("TimeScale_rate" ,timescale_rate);

    }
}
