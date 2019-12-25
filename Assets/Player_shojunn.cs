using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shojunn : MonoBehaviour
{
    Vector3 mpos;
    Vector3 screenToWorldPointPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject game_control = GameObject.Find("Main Camera");
        Game_control game_script = game_control.GetComponent<Game_control>();

        if (game_script.game_state == Game_control.PLAYING)
        {
            Cursor.visible = false;

            //FindObjectByType コンポーネント呼び出し
            mpos = Input.mousePosition;         // Vector3でマウス位置座標を取得する
            mpos.z = 10f;                       // Z軸修正
                                                // マウス位置座標をスクリーン座標からワールド座標に変換する
            screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(mpos);
            // ワールド座標に変換されたマウス座標を代入
            mpos = screenToWorldPointPosition;

            transform.position = mpos;
        }
    }
}
