using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

    AudioSource[] sounds; 

    //他クラスを定義
    public UIController ui;
    
    //移動させるためのコンポーネント
    Rigidbody2D rigid2d;

    //ジャンプ速度
    [SerializeField]
    private float jumpVelocity = 20;

    //ジャンプ減衰係数
    [SerializeField]
    private float downVelocity = 0.8f;

    //ゲームオーバーライン
    private float gameOverLine = -9f;

    // Use this for initialization
    void Start () {

        //Audioコンポーネントを複数取得するためにGetComponentにsをつける
        this.sounds = GetComponents<AudioSource>(); 

        //Rigidbody2Dコンポーネントを入れる
        this.rigid2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<AudioSource>().volume = 0.03f;

        if(this.transform.position.y > -1f)
        {
            GetComponent<AudioSource>().volume = 0;
        }

        //左クリックされたらジャンプする
        if (Input.GetMouseButtonDown(0)) 
        {
            sounds[1].Play();

            //上方向へ力を加える
            this.rigid2d.velocity = new Vector2(0, this.jumpVelocity);

            //クリックを止めたら減衰させる（指が離れる）
            if (Input.GetMouseButton(0))
            {
                if(this.rigid2d.velocity.y > 0)
                {
                    this.rigid2d.velocity *= this.downVelocity;
                }
            }
        }

        //ゲームオーバーラインを越えたらゲームオーバーにする
        if(this.transform.position.x < gameOverLine)
        {
            //爆発音鳴らす
            sounds[2].Play();

            //プレイヤーの破壊
            Destroy(gameObject, 1.5f);

            //UIControllerのゲームオーバー関数を呼び出す
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

            GameObject explosionText = ui.explosionText;

            //デッドバーの不透明度を100%に
            explosionText.GetComponent<Text>().color = new Color(1.0f, 0.03f, 0.62f, 0f);
        }

        //プレイヤーの耳がデッドバーに触れたらゲームオーバー
        //元々は4.5
        if (this.transform.position.y > 4.5)
        {
            //UIControllerのフェイズ２関数を呼び出す
            GameObject.Find("Canvas").GetComponent<UIController>().Phase2();

            bool phase2 = ui.phase2;
            
            if (phase2 == true)
            {
                //爆発音鳴らす
                sounds[2].Play();

                //プレイヤーの破壊
                Destroy(gameObject, 1.5f);

                //プレイヤーを見えなくすることで破壊したことにする（ここではまだ破壊されていない）
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

                //UIControllerのゲームオーバー関数を呼び出す
                GameObject.Find("Canvas").GetComponent<UIController>().GameOver();

                GameObject explosionText = ui.explosionText;

                //デッドバーの不透明度を100%に
                explosionText.GetComponent<Text>().color = new Color(1.0f, 0.03f, 0.62f, 0f);
            }
        }
    }
}
