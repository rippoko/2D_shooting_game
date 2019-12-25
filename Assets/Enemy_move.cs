using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_move : MonoBehaviour
{
    public GameObject pobj;
    private Vector3 ppos;
    private Vector3 epos;
    private Vector2 evec;

    public GameObject e_particle;

    private float efacter = 5;

    System.Random r = new System.Random();

    void Start()
    {
        int max_margin = 100;
        int min_margin = 50;
        epos = transform.position;

        //敵の生成位置
        //x方向の位置
        if ((int)r.Next(2) == 0) epos.x = (float)r.Next((int)(Screen.width + min_margin), (int)(Screen.width + max_margin));
        else epos.x = (float)r.Next((int)(-Screen.width - max_margin), (int)(-Screen.width - min_margin));
        //y方向の位置
        epos.y = (float)r.Next((int)(-Screen.height - max_margin), (int)(Screen.height + max_margin));

        transform.position = epos;  //初期位置設定
    }

    void Update()
    {
        GameObject game_control = GameObject.Find("Main Camera");
        Game_control game_script = game_control.GetComponent<Game_control>();

        if (game_script.game_state == Game_control.PLAYING)
        {
            enemy_move();
        }
    }

    void enemy_move()
    {
        pobj = GameObject.Find("Player");   //プレイヤーのオブジェクトを見つける
        epos = transform.position;          //エネミーの位置を取得
        ppos = pobj.transform.position;     //プレイヤーの位置を取得

        evec = ppos - epos;
        evec.Normalize();
        transform.position += (Vector3)(evec * efacter);
    }

    public void enemy_destroy( bool effect_out )
    {
        Destroy(gameObject);

        if (effect_out)
            enemy_effect();
    }

    public void enemy_effect()
    {
        //エフェクトを発生させる
        Instantiate(e_particle, transform.position, transform.rotation);
    }
}
