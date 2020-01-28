using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCauser : MonoBehaviour {

    //他クラスを定義
    //public PlayerController pc;
    public UIController ui;

    private GameObject explosion;

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {

        //UIControllerのフェイズ2関数を呼び出す
        GameObject.Find("Canvas").GetComponent<UIController>().Phase2();

        bool phase2 = ui.phase2;

        bool isGameOver = ui.isGameOver;

        if (phase2 == true && isGameOver)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
