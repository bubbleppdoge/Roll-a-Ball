using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimEvent : MonoBehaviour {

	public GameObject gameOverPage;
	public Button pauseBtn;

	public void _SetStart()				//设置开始动画事件
	{
		GameObject.Find ("GameController").GetComponent<GameController> ().gamePlay = true;
		pauseBtn.interactable = true;
		gameObject.SetActive (false);
	}

	public void _SetEnd()				//设置结束动画事件
	{
		gameOverPage.SetActive (true);
		gameObject.SetActive (false);
	}
}
