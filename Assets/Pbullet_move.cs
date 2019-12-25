using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pbullet_move : MonoBehaviour
{
    private double s_x = 1;              //弾の進む距離比率X
    private double s_y;              //弾の進む距離比率Y
    private const double SSPEED = 10;           //弾の速さ
    private double dis;
    public double spos_x;           //弾の位置X
    public double spos_y;           //弾の位置Y

    private Vector3 mpos;
    private Vector3 screenToWorldPointPosition;
    private Vector3 bpos;
    private Vector3 ppos;
    private Vector3 bvec;

    private float bfacter = 6;

    public GameObject pobj;
    public GameObject eobj;

    void Start()
    {
        //FindObjectByType コンポーネント呼び出し
        pobj = GameObject.Find("Player");   //プレイヤーのオブジェクトを見つける
        ppos = pobj.transform.position;     //プレイヤーの位置を取得
        mpos = Input.mousePosition;         // Vector3でマウス位置座標を取得する
        mpos.z = 10f;                       // Z軸修正
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(mpos);
        // ワールド座標に変換されたマウス座標を代入
        mpos = screenToWorldPointPosition;

        bvec = mpos - ppos;
        bvec.Normalize();

        transform.position = ppos;  //初期位置
    }

    void Update()
    {
        GameObject game_control = GameObject.Find("Main Camera");
        Game_control game_script = game_control.GetComponent<Game_control>();

        if (game_script.game_state == Game_control.PLAYING)
        {
            pbullet_move();
        }
    }

    void pbullet_move()
    {
        //位置の加算処理
        transform.position += (Vector3)(bvec * bfacter);

        float margin = 100;
        if(transform.position.x >= Screen.width + margin || transform.position.x <= -Screen.width - margin || transform.position.y >= Screen.height + margin || transform.position.y <= -Screen.height - margin)
            //もしも画面外に出たら
            pbullet_destroy();  //自機を消す
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 接触した瞬間の１回のみ呼び出される処理
        if (collision.gameObject.name == "Enemy(Clone)")
        {
            GameObject eobj = GameObject.Find("Enemy(Clone)");
            Enemy_move e_script = eobj.GetComponent<Enemy_move>();
            e_script.enemy_destroy( true );   //敵を消す

            pbullet_destroy();  //自機を消す

            pobj = GameObject.Find("Player");
            Player_move p_script = pobj.GetComponent<Player_move>();
            p_script.p_attack++;
        }
    }

    /*private void OnBecameInvisible()
    {
        //もしも画面から消えたらっていう便利関数
        pbullet_destroy();  //自機を消す
    }*/

    void pbullet_destroy()
    {
        Destroy(gameObject);
        pobj = GameObject.Find("Player");   //プレイヤーのオブジェクトを見つける

        //プレーヤーのSPを回復
        Player_move p_script = pobj.GetComponent<Player_move>();
        p_script.p_sp++;
    }

}
