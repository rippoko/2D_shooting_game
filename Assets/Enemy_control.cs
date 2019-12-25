using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_control : MonoBehaviour
{
    public GameObject e_prefab;
    private int e_time = 120;   //敵生成までの間
    private int e_body_count = 0; //敵が何匹生成されたか 
    private int e_count = 0;

    GameObject gm_3;

    void Start()
    {
    }

    void Update()
    {
        GameObject game_control = GameObject.Find("Main Camera");
        Game_control game_script = game_control.GetComponent<Game_control>();

        if (game_script.game_state == Game_control.PLAYING)
        {
            //まだゲーム中なら
            e_count++;  //カウンター

            //一定時間経過で敵生成
            if (e_count > e_time)
            {
                Instantiate(e_prefab, transform.position, Quaternion.identity);
                e_count = 0;
                e_body_count++;

                //敵５体おきに生成の速さを増やす
                if (e_body_count % 5 == 0 && e_body_count != 0)
                {
                    if (e_time >= 30) e_time -= 10;
                }
            }     
        }
    }
}
