﻿using System.Collections;
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


    public bool AI = false;
    public bool tutorial_mode = false;

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

    int last_attack_direction = -1;
    public GameObject vengeance_aura;

    float ai_delay = 0.25f;
    float current_delay = 0f;

    float distance_moved = 0f;

    public int vengeance_cumulative = 0;
    float[] knock_back_pup = { 0f, 0.25f, 0.4f, 0.55f };
    int[] turns = {0, 2, 3, 4 };
    public int vengeance_turn = 0;

    static float initial_timescale = 0.5f;
    static float max_timescale = 2;
    static float timescale_rate = 0.1f;
    void Start()
    {
        player_center = GameObject.Find("Main Camera").GetComponent<camera_movement>();
        if(player_number == 1)
        {
            other_player = GameObject.Find("Player2").GetComponent<PlayerBehaviour>();
            my_sword = GameObject.Find("Sword1").GetComponent<SwordMovement>();
            original_pos = my_sword.transform.localPosition;
            original_rot = my_sword.transform.localRotation;
            my_sword.my_weilder = this;
            current_delay = 0;
        }
        else if (player_number == 2)
        {
            other_player = GameObject.Find("Player1").GetComponent<PlayerBehaviour>();
            my_sword = GameObject.Find("Sword2").GetComponent<SwordMovement>();
            original_pos = my_sword.transform.localPosition;
            original_rot = my_sword.transform.localRotation;
            my_sword.my_weilder = this;
            current_delay = 0;
        }

        initial_timescale = PlayerPrefs.GetFloat("Initial_timescale");
        max_timescale = PlayerPrefs.GetFloat("Max_timescale");
        timescale_rate = PlayerPrefs.GetFloat("TimeScale_rate");
        Time.timeScale = initial_timescale;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_center.menu_mode)
            return;
        vengeance_aura.transform.localScale = new Vector3(0.35f*vengeance_turn, 0.2f*vengeance_turn, 0);


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

            if(vengeance_turn > 0)
                --vengeance_turn;
            //check if the direction was the same
            //if it is is blocked so turn change
            if(other_player.last_attack_direction == last_attack_direction)
            {

                if (vengeance_turn == 0)
                {
                    turn = !turn;
                    other_player.turn = !other_player.turn;
                    player_center.rotating = true;
                }

                if(Time.timeScale < max_timescale)
                    Time.timeScale += timescale_rate;
                //reset the sword positions and rotation
                my_sword.transform.localPosition = original_pos;
                my_sword.transform.localRotation = original_rot;
                other_player.my_sword.transform.localPosition = other_player.original_pos;
                other_player.my_sword.transform.localRotation = other_player.original_rot;
            }
            else
            {
                hit_anim = true;
                other_player.hit_anim = true;                
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

                float distance_step = Mathf.Abs(value) - Mathf.Abs(distance_moved);

                Vector3 direction_v = new Vector3();

                if (player_number == 2)
                {
                  direction_v.x = distance_step;
                }
                else
                  direction_v.x = -distance_step;

                player_center.focus.transform.position = player_center.focus.transform.position + direction_v + direction_v*knock_back_pup[vengeance_cumulative];
                distance_moved += distance_step;

                return;
            }

        if(vengeance_turn == 0)
            vengeance_cumulative = 0;
        

        if (turn && vengeance_turn == 0)
                vengeance_turn = turns[vengeance_cumulative];

            //reset values
            hit_anim = false;
            my_sword.transform.localPosition = original_pos;
            my_sword.transform.localRotation = original_rot;
            hit_counter = 0;
            distance_moved = 0;
            current_delay = ai_delay;
            last_attack_direction = -1;
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
            {
                direction = 2;
            }
            else if (Input.GetKeyDown("d"))
                direction = 3;

            //Vengeance??
            if (Input.GetKeyDown(KeyCode.LeftShift))
                direction = 4;
        }
        else if(player_number == 2)
        {
            if (AI)
            {
                bool enter_dir = false;
                if (turn)
                {
                  if (current_delay > 1.25f)
                    enter_dir = true;
                }
                else
                {
                  if (current_delay > ai_delay)
                    enter_dir = true;
                }
                if (enter_dir)
                {
                    if (turn)
                        direction = Random.Range(0, 4);

                    else
                    {
                        if (Random.Range(0, 100) % 5 == 0)
                        {
                            direction = other_player.last_attack_direction;
                        }
                        else
                            direction = Random.Range(0, 4);
                    }
                    current_delay = 0;
                }
                else
                {
                    //if its attacking
                    if (turn)
                        current_delay += Time.deltaTime * Random.Range(0.8f, 1f);

                    //if its defending
                    else
                    {
                        if (!other_player.attacking)
                            current_delay = 0;
                        else
                        {
                            current_delay += Time.deltaTime * (0.65f + Random.Range(0f, 0.15f));
                        }       
                    }
                }
            }
            else
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

                //Vengeance??

                if (Input.GetKeyDown(KeyCode.RightShift))
                    direction = 4;
            }
        }

        //if attacks
        if(turn)
        {
            if(direction != -1 && direction != 4)
            {
                my_sword.MoveAttackSword(direction, true);
                last_attack_direction = direction;
                attacking = true;
                current_delay = 0;
            }

            return;
        }


        if (other_player.attacking 
            && other_player.preparation_counter < other_player.preparation_time 
            && direction != -1
            && last_attack_direction <= -1)
        {
            my_sword.MoveBlockSword(direction);
            last_attack_direction = direction;
            current_delay = 0;

            if (direction == 4)
            {
                if(vengeance_cumulative < 3)
                    vengeance_cumulative += 1;
                vengeance_turn = turns[vengeance_cumulative];
            }
        }
        //else defends
    }
}
