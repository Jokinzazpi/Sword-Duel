using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hackerinos : MonoBehaviour
{
  public float offset;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.A))
    {
      transform.position = transform.position + new Vector3(-offset, 0, 0);
    }
    if (Input.GetKey(KeyCode.D))
    {
      transform.position = transform.position + new Vector3(+offset, 0, 0);
    }
  }
}
