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

    public void OnCollisionEnter(Collision collision)
    {
        //check hit by sword
        if (collision.gameObject.tag == "Sword")
        {
            my_weilder.turn = !my_weilder.turn;
        }

    }

    public void MoveAttackSword(int direction, bool prepare)
    {
        if(prepare)
        {
            if (direction == 0)
            {
                transform.position = upblock + new Vector3(0,0.1f,0);
                transform.rotation = upattackrot;
                Debug.Log("Upattack");
            }
            else if (direction == 1)
            {
                transform.position = downblock + new Vector3(0, -0.1f, 0);
                transform.rotation = downattackrot;
            }
            else if (direction == 3)
            {
                transform.position = leftblock + new Vector3(-0.1f, 0, 0);
                transform.rotation = leftattackrot;
            }
            else if (direction == 4)
            {
                transform.position = rightblock + new Vector3(0.1f,0, 0);
                transform.rotation = rightattackrot;
            }
        }
   
        if (direction == 0)
        {
            transform.position = upblock;
            transform.rotation = upattackrot;
        }
        else if (direction == 1)
        {
            transform.position = downblock;
            transform.rotation = downattackrot;
        }
        else if (direction == 3)
        {
            transform.position = leftblock;
            transform.rotation = leftattackrot;
        }
        else if (direction == 4)
        {
            transform.position = rightblock;
            transform.rotation = rightattackrot;
        }
    }

    public void MoveBlockSword(int direction)
    {
        if(direction == 0)
        {
            transform.position = upblock;
            transform.rotation = upblockrot;
            Debug.Log("Upblock");
        }
        else if (direction == 1)
        {
            transform.position = downblock;
            transform.rotation = upblockrot;
        }
        else if (direction == 3)
        {
            transform.position = leftblock;
            transform.rotation = leftblockrot;
        }
        else if (direction == 4)
        {
            transform.position = rightblock;
            transform.rotation = leftblockrot;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
