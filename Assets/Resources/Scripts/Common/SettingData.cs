using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingData : MonoBehaviour {

	public static SettingData Instance;	//单例，场景间数据传输

	public int mapWidth;				//地图宽
	public int mapLength;				//地图长
	public int moveSpeed;				//小球速度
	public int camMoveSpeed;			//摄像机跟随速度
	public Dictionary<string, int> rankDict = new Dictionary<string, int> ();			//排行榜数据

	public int GameTime; 				//游戏时间
	public int SpawnWaveTime;			//波次间隔
	public int MaxWaveSpawnNum;			//一波最大数量

	void Awake()
	{
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else {
			Destroy (gameObject);
		}
	}
}
