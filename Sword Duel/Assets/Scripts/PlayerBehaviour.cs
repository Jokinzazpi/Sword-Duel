using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public bool turn = false;
    public int player_number = 1;

    ////Get Hit anim
    Vector3 currentpos;
    public float distance_hit_back = 5;
    bool hit_anim = false;
    public float hit_time = 0.1f;
    float hit_counter = 0f;
    ////Attack Anim
    
    [HideInInspector]
    public bool attacking = false;
    public float attack_time = 1f;
    float attack_counter = 0f;
    public float preparation_time = 0.5f;
    float preparation_counter = 0f;


    public Vector3 original_pos;
    public Quaternion original_rot;
    SwordMovement my_sword;
    PlayerBehaviour other_player;

    int last_attack_direction = 0;
    void Start()
    {
        original_pos = transform.GetChild(0).transform.position;
        original_rot = transform.GetChild(0).transform.rotation;

        my_sword = transform.GetChild(0).GetComponent<SwordMovement>();
        if(player_number == 1)
            other_player = GameObject.Find("Player2").GetComponent<PlayerBehaviour>();
        else if (player_number == 2)
            other_player = GameObject.Find("Player1").GetComponent<PlayerBehaviour>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        //check hit by sword
        if(collision.gameObject.tag == "Sword")
        {
            //if does step_back
            currentpos = transform.position;
            hit_anim = true;
            collision.gameObject.GetComponent<PlayerBehaviour>().hit_anim = true;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(attacking)
        {
            if (preparation_counter < preparation_time)
            {
                preparation_counter += Time.deltaTime;
                return;
            }

            my_sword.MoveAttackSword(last_attack_direction, false);

            if (attack_counter < attack_time)
            {
                attack_counter += Time.deltaTime;
                return;
            }

            preparation_counter = 0f;
            attack_counter = 0f;
            attacking = false;
            return;
        }

        if (hit_anim)
        {
            if (hit_counter < hit_time)
            {
                hit_counter += Time.deltaTime;
                return;
            }

            if(turn)
                transform.position = transform.position + Vector3.Scale(transform.forward, new Vector3(distance_hit_back, distance_hit_back, distance_hit_back));
            else
                transform.position = transform.position - Vector3.Scale(transform.forward, new Vector3(distance_hit_back, distance_hit_back, distance_hit_back));

            hit_anim = false;
            transform.GetChild(0).transform.position = original_pos;
            transform.GetChild(0).transform.rotation = original_rot;
            hit_counter = 0;

            return;
        }

        int direction = -1;
        if(player_number == 1)
        {
            if (Input.GetKeyDown("w"))
                direction = 0;
            else if (Input.GetKeyDown("s"))
                direction = 1;
            else if (Input.GetKeyDown("a"))
                direction = 2;
            else if (Input.GetKeyDown("d"))
                direction = 3;
        }
        else if(player_number == 2)
        {
            if (Input.GetKeyDown("i"))
                direction = 0;
            else if (Input.GetKeyDown("j"))
                direction = 1;
            else if (Input.GetKeyDown("k"))
                direction = 2;
            else if (Input.GetKeyDown("l"))
                direction = 3;
        }

        //if attacks
        if(turn)
        {
            if(direction != -1)
            {
                my_sword.MoveAttackSword(direction, true);
                last_attack_direction = direction;
                attacking = true;
            }

            return;
        }
        if (other_player.attacking 
            && other_player.preparation_counter < other_player.preparation_time 
            && direction != -1)
            my_sword.MoveBlockSword(direction);
        //else defends
    }
}
