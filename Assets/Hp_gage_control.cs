using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp_gage_control : MonoBehaviour
{
    GameObject player_object;
    Player_move player_script;
    Vector3 sp_gage_scale;
    float sp_gage_scale_orign_x, sp_gage_scale_orign_y;
    float sp_one_mater_x;

    void Start()
    {
        //p_point_max = Player_move.P_SP
        player_object = GameObject.Find("Player");
        player_script = player_object.GetComponent<Player_move>();
        sp_gage_scale_orign_x = transform.localScale.x;
        sp_gage_scale_orign_y = transform.localScale.y;
        sp_one_mater_x = sp_gage_scale_orign_x / Player_move.P_HP;
    }

    // Update is called once per frame
    void Update()
    {
        //p_pint = player_script.p_sp
        transform.localScale = new Vector3(sp_one_mater_x * player_script.p_hp, sp_gage_scale_orign_y, 0);
    }
}
