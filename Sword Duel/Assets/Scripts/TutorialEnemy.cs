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
    camera_movement player_center;

    int last_attack_direction = 0;

    public int AI_always_dir;

    //JokinVars
    float distance_moved = 0f;
    void Start()
    {
        player_center = GameObject.Find("Main Camera").GetComponent<camera_movement>();
        if (player_number == 1)
        {
            other_player = GameObject.Find("Player2").GetComponent<TutorialEnemy>();
            my_sword = GameObject.Find("Sword1").GetComponent<SwordMovement>();
            original_pos = my_sword.transform.localPosition;
            original_rot = my_sword.transform.localRotation;
            my_sword.my_weilder = this;
        }
        else if (player_number == 2)
        {
            other_player = GameObject.Find("Player1").GetComponent<TutorialEnemy>();
            my_sword = GameObject.Find("Sword2").GetComponent<SwordMovement>();
            original_pos = my_sword.transform.localPosition;
            original_rot = my_sword.transform.localRotation;
            my_sword.my_weilder = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
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


            //check if the direction was the same
            //if it is is blocked so turn change
            if (other_player.last_attack_direction == last_attack_direction)
            {
                //    turn = !turn;
                //    other_player.turn = !other_player.turn;
                //    player_center.rotating = true;
                //    //reset the sword positions and rotation
                //    my_sword.transform.localPosition = original_pos;
                //    my_sword.transform.localRotation = original_rot;
                //    other_player.my_sword.transform.localPosition = other_player.original_pos;
                //    other_player.my_sword.transform.localRotation = other_player.original_rot;
            }
            else
            {
                hit_anim = true;
                other_player.hit_anim = true;
                player_center.focus.transform.position = player_center.focus.transform.position + Vector3.Scale(player_center.focus.transform.right, new Vector3(distance_hit_back, distance_hit_back, distance_hit_back));

            }

            preparation_counter = 0f;
            attack_counter = 0f;
            attacking = false;
            //reset to different integers
            other_player.last_attack_direction = -1;
            last_attack_direction = -2;

            return;
        }

        if (hit_anim)
        {
            if (hit_counter < hit_time)
            {
                float value = Mathf.SmoothStep(0f, distance_hit_back, hit_counter / hit_time);
                hit_counter += Time.deltaTime;

                if (!turn)
                    return;

                float distance_step = value - distance_moved;

                Vector3 direction_v = new Vector3();

                if (player_number == 2)
                {
                    direction_v.x = 1;
                }
                else
                    direction_v.x = -1;

                player_center.focus.transform.position = player_center.focus.transform.position + direction_v;
                distance_moved += distance_step;

                return;
            }

            hit_anim = false;
            my_sword.transform.localPosition = original_pos;
            my_sword.transform.localRotation = original_rot;
            hit_counter = 0;
            distance_moved = 0;
            return;
        }

        int direction = -1;
        if (player_number == 1)
        {
            direction = AI_always_dir;
        }
        else if (player_number == 2)
        {
            if (Input.GetKeyDown("i"))
                direction = 0;
            else if (Input.GetKeyDown("k"))
                direction = 1;
            else if (Input.GetKeyDown("j"))
            {
                direction = 2;
            }
            else if (Input.GetKeyDown("l"))
                direction = 3;
        }

        //if attacks
        if (turn)
        {
            if (direction != -1)
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
        {
            my_sword.MoveBlockSword(direction);
            last_attack_direction = direction;
        }
        //else defends
    }
}
