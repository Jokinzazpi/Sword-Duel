using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour
{
  public GameObject focus;
  public float offset;
  public float time;
  public int lerp_effect_scale;
  public bool menu_mode;
  
    [HideInInspector]
  public bool rotating = false;
  float current_rotation = 0;
  float current_time = 0;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (menu_mode)
    {

      transform.RotateAround(focus.transform.position, new Vector3(0, 1, 0), -offset);
      transform.LookAt(focus.transform);
      return;
    }

    float step = 180 * 0.016f / time;

    if (!rotating)
    {
      if (Input.GetKey(KeyCode.Space))
      {
        rotating = true;
        current_rotation = 0;
        current_time = 0;
      }
    }

    if (rotating)
    {
      float multiplier = Mathf.SmoothStep(0f, time, current_time/time);

      float this_frame = Mathf.SmoothStep(0f, 180f, multiplier/time);

      float temp_step = this_frame - current_rotation;

      transform.RotateAround(focus.transform.position, new Vector3(0, 1, 0), -temp_step);

      if (current_rotation >= 180)
        rotating = false;


      current_rotation += temp_step;
      current_time += Time.deltaTime;
    }

    transform.LookAt(focus.transform);
  }
}
