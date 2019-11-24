using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    [HideInInspector]
    public PlayerBehaviour my_weilder;
    // Start is called before the first frame update

    public Vector3 upblock = new Vector3(-0.722f, 0.55f, 0.6f);
    public Vector3 downblock = new Vector3(-0.722f, -0.2f, 0f);
    public Vector3 leftblock = new Vector3(-0.722f, 0.089f, -0.6f);
    public Vector3 rightblock = new Vector3(-0.722f, 0.089f, 0.6f);

    public Quaternion upblockrot = new Quaternion(0,90,0, 0);
    public Quaternion leftblockrot=new Quaternion(0,0,90, 0);


    public Quaternion upattackrot = new Quaternion(0, 90, 30, 0);
    public Quaternion leftattackrot = new Quaternion(30, 0, 90, 0);
    public Quaternion downattackrot = new Quaternion(0, 90, -30, 0);
    public Quaternion rightattackrot = new Quaternion(-30, 0, 90, 0);
    void Start()
    {
    }

    public void MoveAttackSword(int direction, bool prepare)
    {
        if(prepare)
        {
            if (direction == 0)
            {
                transform.localPosition = upblock + new Vector3(0,0.1f,0);
                transform.localRotation = upattackrot;
                Debug.Log("Upattack");
            }
            else if (direction == 1)
            {
                transform.localPosition = downblock + new Vector3(0, -0.1f, 0);
                transform.localRotation = downattackrot;
            }
            else if (direction == 3)
            {
                transform.localPosition = leftblock + new Vector3(-0.1f, 0, 0);
                transform.localRotation = leftattackrot;
            }
            else if (direction == 4)
            {
                transform.localPosition = rightblock + new Vector3(0.1f,0, 0);
                transform.localRotation = rightattackrot;
            }
        }
   
        if (direction == 0)
        {
            transform.localPosition = upblock;
            transform.localRotation = upattackrot;
        }
        else if (direction == 1)
        {
            transform.localPosition = downblock;
            transform.localRotation = downattackrot;
        }
        else if (direction == 3)
        {
            transform.localPosition = leftblock;
            transform.localRotation = leftattackrot;
        }
        else if (direction == 4)
        {
            transform.localPosition = rightblock;
            transform.localRotation = rightattackrot;
        }
    }

    public void MoveBlockSword(int direction)
    {
        if(direction == 0)
        {
            transform.localPosition = upblock;
            transform.localRotation = upblockrot;
            Debug.Log("Upblock");
        }
        else if (direction == 1)
        {
            transform.localPosition = downblock;
            transform.localRotation = upblockrot;
        }
        else if (direction == 3)
        {
            transform.localPosition = leftblock;
            transform.localRotation = leftblockrot;
        }
        else if (direction == 4)
        {
            transform.localPosition = rightblock;
            transform.localRotation = leftblockrot;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
