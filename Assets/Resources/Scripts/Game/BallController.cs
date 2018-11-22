using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	[Range(4,15)]
	public float moveSpeed = 4.0f;							//移动速度（实际为施加在对象上的力）

	private Rigidbody rb;									//刚体组件
	private GameController gameCtrl;

	void Start () {
		moveSpeed = SettingData.Instance.moveSpeed;			//加载设置好的速度

		rb = GetComponent<Rigidbody> ();
		gameCtrl = GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	void FixedUpdate () {									//物理运动相关使用FixedUpdate
		if (gameCtrl.gamePlay) {
			float h = Input.GetAxis ("Horizontal");			//键盘控制事件：水平
			float v = Input.GetAxis ("Vertical");			//键盘控制事件：垂直
			rb.AddForce (new Vector3 (h, 0, v) * moveSpeed);//对对象施加指定方向上的力
		}
		if (transform.position.y <= 0) {					//（作弊后）速度过快时，容易穿模跌出地图，重置位置
			transform.position = new Vector3 (0f, 0.5f, 0f);
			rb.velocity = Vector3.zero;
		}
	}

	void OnTriggerEnter(Collider other)						//触发器，用于小球拾取方块
	{
		if (other.gameObject.tag == "PickUp" && gameCtrl.gamePlay) {
			gameCtrl.playerScore++;
			Destroy (other.gameObject);
		}
	}
}
