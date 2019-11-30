using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float lookvector = Camera.main.transform.position.y - transform.position.y;
        transform.LookAt(Camera.main.transform.position-new Vector3(0f, lookvector, 0f), Vector3.up);
    }
}
