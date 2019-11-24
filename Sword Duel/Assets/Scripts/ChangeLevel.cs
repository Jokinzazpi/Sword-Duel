using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
  public string scene_name;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void TaskOnClick()
    {
     SceneManager.LoadScene(scene_name);
    }
}
