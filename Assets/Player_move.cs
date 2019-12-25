using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{
    private float speed_factor = 10;
    public const int P_HP = 10;    //プレイヤーが撃てる弾の残機のMAX
    public const int P_SP = 10;    //プレイヤーが撃てる弾の残機のMAX
    public int p_hp = 10;
    public int p_attack = 0;
    public int p_sp = 10;    //プレイヤーが撃てる弾の残機
   

    public int player_state;     //プレイヤーの状態
    public const int ENABLE = 0;   //プレイヤーが生きている状態
    public const int DISABLE = 1;  //プレイヤーが死んでいる状態

    public GameObject p_particle;

    private bool damageing = false; //ダメージを受けている間のフラグ
    private const int damage_time = 60;
    private int damage_count = 0;

    private bool die_pass;

    // Start is called before the first frame update
    void Start()
    {
        player_state = ENABLE;
        p_hp = P_HP;
        p_sp = P_SP;
        die_pass = false;
        GetComponent<Renderer>().material.color = Color.white;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (player_state)
        {
            case ENABLE:
                //プレイヤーが生きているときに実行
                player_move();
                if (damageing) player_dmaged_effect();
                break;

            case DISABLE:
                //プレイヤーが死んでいるときに実行
                if( !die_pass )player_die();
                break;

            default:
                break;
        }
    }

    void player_move()
    {
        Vector2 player_speed = Vector2.zero; //new Vector2(0,0);

        if (Input.GetKey(KeyCode.W))
            player_speed += Vector2.up; // new  Vector2(0,1);
        if (Input.GetKey(KeyCode.A))
            player_speed += Vector2.left; // new  Vector2(-1,0);
        if (Input.GetKey(KeyCode.S))
            player_speed += Vector2.down; // new  Vector2(0,-1);
        if (Input.GetKey(KeyCode.D))
            player_speed += Vector2.right; // new  Vector2(1,0);

        player_speed *= speed_factor;

        transform.position += (Vector3)player_speed;
    }

    void player_damaged()
    {
        p_hp--; //HPを減らす

        //ダメージを受けたっぽいエフェクト
        damageing = true;

        //プレイヤーが死んでいないか判定
        if (p_hp <= 0)
            player_state = DISABLE;
    }

    void player_dmaged_effect()
    {
        damage_count++;

        //青と白を入れ替えて点滅しているように見せる
        if (damage_count % 10 == 0)
            GetComponent<Renderer>().material.color = Color.blue;
        else if (damage_count % 10 == 5)
            GetComponent<Renderer>().material.color = Color.white;

        //ダメージカウントが終わったら
        if (damage_count >= damage_time)
        {
            damageing = false;
            damage_count = 0;
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    void player_die()
    {
        GameObject game_control = GameObject.Find("Main Camera");
        Game_control game_script = game_control.GetComponent<Game_control>();
        game_script.game_state = Game_control.FINISH;

        GetComponent<Renderer>().material.color = Color.blue;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        //パーティクルを発生させる
        Instantiate(p_particle, transform.position, transform.rotation);
        die_pass = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 接触した瞬間の１回のみ呼び出される処理
        if (collision.gameObject.name == "Enemy(Clone)")
        {
            Enemy_move e_script = collision.gameObject.GetComponent<Enemy_move>();
            e_script.enemy_destroy( false );   //敵を消す

            //自分にダメージを与える
            player_damaged();
        }
    }
}
