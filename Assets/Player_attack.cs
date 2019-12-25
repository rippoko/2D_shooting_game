using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_attack : MonoBehaviour
{
    public GameObject pb_prefab;
    Player_move player_move_s;

    void Start()
    {
        player_move_s = GetComponent<Player_move>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject game_control = GameObject.Find("Main Camera");
        Game_control game_script = game_control.GetComponent<Game_control>();

        if (game_script.game_state == Game_control.PLAYING)
        {
            //まだゲーム中なら
            if (Input.GetKeyDown(KeyCode.Space) && player_move_s.p_sp > 0)
            {
                //弾インスタンス生成
                Instantiate(pb_prefab, transform.position, Quaternion.identity);
                player_move_s.p_sp--;
            }
        }
    }
}
