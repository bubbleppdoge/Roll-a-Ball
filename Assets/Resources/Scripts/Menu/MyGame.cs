using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGame : MonoBehaviour {

	public InputField gameTime;			//游戏时间
	public InputField spawnWavaTime;	//波次间隔
	public InputField maxWaveSpawnNum;	//一波的数量
	public InputField moveSpeed;		//小球速度

	void Start () {
		gameTime.text = "60";
		spawnWavaTime.text = "5";
		maxWaveSpawnNum.text = "7";
		moveSpeed.text = "";
	}

	public void SetMyData()
	{
		SettingData.Instance.GameTime = int.Parse (gameTime.text);
		SettingData.Instance.SpawnWaveTime = int.Parse (spawnWavaTime.text);
		SettingData.Instance.MaxWaveSpawnNum = int.Parse (maxWaveSpawnNum.text);
		SettingData.Instance.moveSpeed = moveSpeed.text == "" ? SettingData.Instance.moveSpeed : int.Parse (moveSpeed.text);
	}
}
