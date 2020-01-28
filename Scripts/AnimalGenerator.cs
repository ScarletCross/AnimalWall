using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalGenerator : MonoBehaviour
{

    //他クラスを定義
    public UIController ui;

    //Public
    public GameObject[] animals;


    //Private
    //時間計測変数
    private float delta = 0f;

    //キューブの生成間隔
    [SerializeField]
    private float span = 1.0f;

    //キューブの生成位置：X座標
    private float genPosX = 7f;

    //キューブの生成位置オフセット
    private float offsetY = 0.3f;

    //キューブ縦方向の間隔
    private float spaceY = 6.9f;

    //キューブの生成位置オフセット
    private float offsetX = 0.5f;

    //キューブ横方向の間隔
    private float spaceX = 0.4f;

    //キューブ生成上限
    private int max = 4;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;

      
        //span以上時間が経過したとき
        if (this.delta > this.span )
        {
            
            //deltaを初期化
            this.delta = 0f;

            //キューブの数をランダムで決定
            int n = Random.Range(1, max + 1);

            //指定した数のキューブを生成する
            for (int i = 0; i < n; i++)
            {
                //キューブ生成
                int number = Random.Range(0, animals.Length);
                GameObject cubes = Instantiate(animals[number], transform.position, transform.rotation);

                cubes.transform.position = new Vector2(this.genPosX, this.offsetY + i * this.spaceY);
            }

            //次のキューブ生成までの時間を決める
            this.span = this.offsetX + this.spaceX * n;
            
            //UIControllerのフェイズ1関数を呼び出す
            GameObject.Find("Canvas").GetComponent<UIController>().Phase1();

            bool phase1 = ui.phase1;

            if (phase1 == true)
            {
                this.max = 5;                            
            }        
        }
    }  
}
