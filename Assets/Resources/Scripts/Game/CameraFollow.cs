using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;		//跟随对象（小球）

	[Range(1, 10)]
	public float followSpeed = 1f;	//跟随速度

	private Vector3 offset;			//跟目标的位置偏移量

	void Start()
	{
		SetFollowSpeed(SettingData.Instance.camMoveSpeed);	//加载菜单界面的跟随速度
		offset = transform.position - target.position;
	}

	public void SetFollowSpeed(int speed)
	{
		followSpeed = speed;
	}

	void FixedUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * followSpeed);
	}
}
