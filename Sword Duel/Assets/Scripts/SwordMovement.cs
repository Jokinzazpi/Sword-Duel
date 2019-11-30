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

    public Vector3 upblockrot = new Vector3(0,90,0);
    public Vector3 leftblockrot=new Vector3(0,0,90);


    public Vector3 upattackrot = new Vector3(0, 90, 30);
    public Vector3 leftattackrot = new Vector3(30, 0, 90);
    public Vector3 downattackrot = new Vector3(0, 90, -30);
    public Vector3 rightattackrot = new Vector3(-30, 0, 90);

    void Start()
    {
    }

    public void MoveAttackSword(int direction, bool prepare)
    {

        var materialrend = GetComponent<Renderer>();
        if (prepare)
        {
            if (direction == 0)
            {
                transform.localPosition = upblock + new Vector3(0,0.2f,0);
                transform.localRotation = Quaternion.Euler(upattackrot);
                materialrend.material.SetColor("_Color", Color.green);
            }
            else if (direction == 1)
            {
                transform.localPosition = downblock + new Vector3(0, -0.2f, 0);
                transform.localRotation = Quaternion.Euler(downattackrot);
                materialrend.material.SetColor("_Color", Color.cyan);
            }
            else if (direction == 2)
            {
                transform.localPosition = rightblock + Vector3.Scale(new Vector3(0.6f, 0.6f, 0.6f),transform.forward);
                transform.localRotation = Quaternion.Euler(leftattackrot);
                materialrend.material.SetColor("_Color", Color.magenta);

            }
            else if (direction == 3)
            {
                transform.localPosition = leftblock + Vector3.Scale(new Vector3(-0.6f,-0.6f, -0.6f), transform.forward);
                transform.localRotation = Quaternion.Euler(rightattackrot);
                materialrend.material.SetColor("_Color", Color.yellow);
            }
            return;
        }

        materialrend.material.SetColor("_Color", Color.white);

        if (direction == 0)
        {
            transform.localPosition = upblock;
            transform.localRotation = Quaternion.Euler(upattackrot);
        }
        else if (direction == 1)
        {
            transform.localPosition = downblock;
            transform.localRotation = Quaternion.Euler(downattackrot);
        }
        else if (direction == 2)
        {
            transform.localPosition = rightblock;
            transform.localRotation = Quaternion.Euler(leftattackrot);
        }
        else if (direction == 3)
        {
            transform.localPosition = leftblock;
            transform.localRotation = Quaternion.Euler(rightattackrot);
        }
    }

    public void MoveBlockSword(int direction)
    {
        if(direction == 0)
        {
            transform.localPosition = upblock;
            transform.localRotation = Quaternion.Euler(upblockrot);
        }
        else if (direction == 1)
        {
            transform.localPosition = downblock;
            transform.localRotation = Quaternion.Euler(upblockrot);
        }
        else if (direction == 2)
        {
            transform.localPosition = leftblock;
            transform.localRotation = Quaternion.Euler(leftblockrot);
        }
        else if (direction == 3)
        {
            transform.localPosition = rightblock;
            transform.localRotation = Quaternion.Euler(leftblockrot);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
