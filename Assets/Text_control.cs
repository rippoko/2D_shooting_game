using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_control : MonoBehaviour
{
    public int game_state;
    public const int PLAYING = 0;
    public const int FINISH = 1;
    public const int RESET = 2;
    public GameObject text_attack = null; // Textオブジェクト
    public GameObject text_hp = null; // Textオブジェクト
    public GameObject text_gameover = null; // Textオブジェクト
    public GameObject text_presskey = null; // Textオブジェクト
    public GameObject text_sp = null; // Textオブジェクト
    private GameObject player_obj;
    private Player_move player_script;
    public GameObject hp_prefab;
    public GameObject sp_prefab;
    Sp_gage_control hp_gage_script;
    Sp_gage_control sp_gage_script;
    public GameObject hpobj;

    // Use this for initialization
    void Start () {
        game_state = PLAYING;
        player_obj = GameObject.Find("Player");
        player_script = player_obj.GetComponent<Player_move>();

        /*Vector3 hp_prefab_position = new Vector3( -480, 380, 0 );
        Vector3 sp_prefab_position = new Vector3(-480, 280, 0);

        Instantiate(hp_prefab, hp_prefab_position, Quaternion.identity);
        hp_gage_script = hp_prefab.GetComponent<Sp_gage_control>();
        hp_gage_script.p_point = player_script.p_hp;
        hp_gage_script.p_point_max = Player_move.P_HP;

        Instantiate(sp_prefab, sp_prefab_position, Quaternion.identity);
        sp_gage_script = sp_prefab.GetComponent<Sp_gage_control>();
        sp_gage_script.p_point = player_script.p_sp;
        sp_gage_script.p_point_max = Player_move.P_SP;*/
    }
	
	// Update is called once per frame
	void Update () {
        switch (game_state)
        {
            case PLAYING:
                play_display();
                break;

            case FINISH:
                end_display();
                reset_commond();
                break;

            case RESET:
                game_reset();
                break;

            default:
                break;
        }
    }

    void play_display()
    {
        player_script = player_obj.GetComponent<Player_move>();

        // オブジェクトからTextコンポーネントを取得
        Text text_attack_dis = text_attack.GetComponent<Text>();
        Text text_hp_dis = text_hp.GetComponent<Text>();
        Text text_gameover_dis = text_gameover.GetComponent<Text>();
        Text text_presskey_dis = text_presskey.GetComponent<Text>();
        Text text_sp_dis = text_sp.GetComponent<Text>();

        // テキストの表示を入れ替える
        text_attack_dis.text = "撃破：" + player_script.p_attack;
        text_hp_dis.text = "残機：" + player_script.p_hp;
        text_gameover_dis.text = " ";
        text_presskey_dis.text = " ";
        text_sp_dis.text = "弾数：" + player_script.p_sp;

        /*hp_gage_script.p_point = player_script.p_hp;
        sp_gage_script.p_point = player_script.p_sp;*/
    }

    void end_display()
    {
        // オブジェクトからTextコンポーネントを取得
        Text text_attack_dis = text_attack.GetComponent<Text>();
        Text text_hp_dis = text_hp.GetComponent<Text>();
        Text text_gameover_dis = text_gameover.GetComponent<Text>();
        Text text_presskey_dis = text_presskey.GetComponent<Text>();
        Text text_sp_dis = text_sp.GetComponent<Text>();

        // テキストの表示を入れ替える
        text_attack_dis.text = " ";
        text_hp_dis.text = " ";
        text_gameover_dis.text = "GAME OVER";
        text_presskey_dis.text = "Please press key【R】";
        text_sp_dis.text = " ";

        hpobj = GameObject.Find("Sp_gage");
        //hpobj.SetActive(false);
        hpobj.GetComponent<Renderer>().material.color = Color.blue;
        hpobj.transform.position = new Vector3(hpobj.transform.position.x, hpobj.transform.position.y, -10);
    }

    void reset_commond()
    {
        if (Input.GetKeyDown(KeyCode.R))
            game_state = RESET;
    }

    void game_reset()
    {
        hpobj = GameObject.Find("Sp_gage");
        //hpobj.SetActive(true);
        hpobj.GetComponent<Renderer>().material.color = Color.white;
        SceneManager.LoadScene(0);

        //もう一度ゲームスタート
        game_state = PLAYING;
    }
}
