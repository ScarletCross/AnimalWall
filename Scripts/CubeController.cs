using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {
  
    //キューブの移動速度
    public float speed = -0.2f;

    //消失位置
    private float deadLine = -10f;
    
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //キューブを移動させる
        transform.Translate(this.speed, 0, 0);

        //画面外で破壊する
        if(transform.position.x < deadLine)
        {
            Destroy(gameObject);
        }
    }   
}
