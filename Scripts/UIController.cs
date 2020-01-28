using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    //ゲームオーバーテキストの定義
    private GameObject gameOverText;

    //デッドバーを定義
    private GameObject deadBar;

    //スコアテキストの定義
    private GameObject scoreText;

    //リスタートテキストの定義
    private GameObject reStartText;

    //爆発テキストの定義
    public GameObject explosionText;

    //走った距離
    private float distance = 0f;


    //Public
    //走るスピード（フレームごとに加算されるスコア）
    public float speedScore = 0.06f;

    //フェイズ1判定
    public bool phase1 = false;

    //フェイズ２判定
    public bool phase2 = false;

    //ゲームオーバー判定
    public bool isGameOver = false;

   
	// Use this for initialization
	void Start () {

        //シーンビューからテキストを持ってくる
        this.scoreText = GameObject.Find("Score");
        this.gameOverText = GameObject.Find("GameOver");
        this.reStartText = GameObject.Find("ReStart");
        this.explosionText = GameObject.Find("Explosion");

        //始めは透明でスタートする
        this.deadBar = GameObject.Find("DeadBar");
        this.deadBar.GetComponent<Image>().color = new Color(1.0f, 0.03f, 0.62f, 0f);


	}
	
	// Update is called once per frame
	void Update () {
		
        if(this.isGameOver == false)
        {
            //走った距離を更新
            this.distance += this.speedScore * 10;

            //走った距離を表示する
            this.scoreText.GetComponent<Text>().text = "Score:" + distance.ToString("F2") + "m";
         
            if(this.distance >= 1000)
            {
                //プレイヤーが疲れて、走るスピードが遅くなる
                this.speedScore = 0.05f;
                this.distance += this.speedScore * 10;
                //走った距離を表示する
                this.scoreText.GetComponent<Text>().text = "Score:" + distance.ToString("F2") + "m";
       
                //デッドバーの不透明度を20%に
                this.deadBar.GetComponent<Image>().color = new Color(1.0f, 0.03f, 0.62f, 0.33f);
                this.explosionText.GetComponent<Text>().text = "なんだろう？";

                //2000  ここで壁の数が増えるようになる
                if (this.distance >= 2000)
                {
                    this.speedScore = 0.04f;
                    this.distance += this.speedScore * 10;

                    this.scoreText.GetComponent<Text>().text = "Score:" + distance.ToString("F2") + "m";
   
                    this.deadBar.GetComponent<Image>().color = new Color(1.0f, 0.03f, 0.62f, 0.66f);

                    this.explosionText.GetComponent<Text>().text = "嫌な予感がする……";

                    //3000 ここで爆発バーを追加
                    if (this.distance >= 3000)
                    {
                        this.speedScore = 0.03f;
                        this.distance += this.speedScore * 10;

                        this.scoreText.GetComponent<Text>().text = "Score:" + distance.ToString("F2") + "m";

                        this.deadBar.GetComponent<Image>().color = new Color(1.0f, 0.03f, 0.62f, 1.0f);

                        this.explosionText.GetComponent<Text>().text = "バーに触れると爆発するよ☆";
                    }
                }
            }
        }

        if(this.isGameOver == true)
        {
            //クリックでゲームシーンを再読み込み
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("GameScene");
            }
        }

	}
    //ゲームオーバー関数
    public void GameOver()
    {
        //ゲームオーバーになると画面上に「ざんねんっ！」と表示させる
        this.gameOverText.GetComponent<Text>().text = "ざんねんっ！";       

        this.reStartText.GetComponent<Text>().text = "クリックでリスタート！";

        this.isGameOver = true;

    }

    //フェイズ1関数（生成数を増やす）
    public void Phase1()
    {
        if(this.distance >= 2000)
        {
            this.phase1 = true;
        }
    }

    //フェイズ２関数（爆発バーの追加）
    public void Phase2()
    {
        if(this.distance >= 3000)
        {
           this.phase2 = true;
        }
    } 
}
