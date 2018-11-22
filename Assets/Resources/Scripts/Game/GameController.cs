using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[HideInInspector]
	public int playerScore = 0;			//游戏分数，通过拾取黄色方块获得
	[HideInInspector]
	public bool gamePlay = true;		//游戏进行中的标志

	public GameObject map;				//地图物体

	public Text scoreText;				//游戏界面中分数文本
	public Text timeText;				//游戏界面中时间文本
	public Button pauseBtn;				//游戏界面中暂停按钮（游戏过程中才能交互）

	public GameObject gameOverPage;		//游戏结束弹出页，用来保存单局数据或退回菜单界面
	public Text finalScoreText;			//游戏结束弹出页中分数文本

	public GameObject startGameAnim;	//游戏开始动画
	public GameObject endGameAnim;		//游戏结束动画

	public GameObject pickUpPre;		//黄色方块预制体
	public GameObject pickUpParent;		//黄色方块生成后放置的位置

	private float mapWidth = 20f;		//初始地图大小
	private float mapLength = 20f;

	private float curTime;				//当前游戏时间

	public DeveloperData deData;		//开发者数据（作弊）

	void Awake()
	{
		deData.GameTime = SettingData.Instance.GameTime;
		deData.SpawnWaveTime = SettingData.Instance.SpawnWaveTime;
		deData.MaxWaveSpawnNum = SettingData.Instance.MaxWaveSpawnNum;

		SetMap (SettingData.Instance.mapWidth, SettingData.Instance.mapLength);						//设置地图大小
	}

	void Start()
	{
		Time.timeScale = 1;																			//初始化游戏各项数据
		playerScore = 0;
		scoreText.text = "0";
		timeText.text = "Time: 60";
		finalScoreText.text = "0";
		curTime = deData.GameTime;
		pauseBtn.interactable = false;
		startGameAnim.SetActive (true);
		endGameAnim.SetActive (false);
		gameOverPage.SetActive (false);
		gamePlay = false;

		StartCoroutine ("PickUpSpawn");																//协程调用，黄色方块生成
	}

	void Update()
	{
		if(gamePlay)																				//开始游戏后倒计时
			curTime -= Time.deltaTime;

		if (curTime > 0) {																			//游戏过程中更新相应UI
			scoreText.text = playerScore.ToString ();
			timeText.text = "Time: " + ((int)curTime).ToString ();
		}
		else if(gamePlay && curTime <= 0)															//游戏结束
			GameOver ();
	}

	void SetMap(float width, float length)
	{
		mapWidth = width;
		mapLength = length;
		map.transform.localScale = new Vector3 (width / 10, 1f, length / 10);						//动态设置地图大小
	}

	void GameOver()																					//游戏结束后播放游戏结束动画，显示结束交互画面
	{
		gamePlay = false;
		timeText.text = "Time: 0";
		endGameAnim.SetActive (true);
		pauseBtn.interactable = false;
		finalScoreText.text = scoreText.text;
	}

	IEnumerator PickUpSpawn()																		//按波次生成黄色方块
	{
		while (curTime >= 0) {
			int spawnNum = Random.Range ((int)(deData.MaxWaveSpawnNum / 2), deData.MaxWaveSpawnNum);//数量随机，位置随机
			for (int i = 0; i < spawnNum; i++) {
				float spawnXPos = Random.Range (-mapWidth / 2 + 0.5f, mapWidth / 2 - 0.5f) ;
				float spawnZPos = Random.Range (-mapLength / 2 + 0.5f, mapLength / 2 - 0.5f);
				Instantiate (pickUpPre, new Vector3 (spawnXPos, 0.5f, spawnZPos), pickUpPre.transform.rotation, pickUpParent.transform);
			}
			yield return new WaitForSeconds (deData.SpawnWaveTime);									//波次间隔；
		}
		yield return 0;
	}

	[System.Serializable]
	public struct DeveloperData
	{
		public float MaxSize;			//地图最大范围
		public float MinSize;			//地图最小范围
		public int GameTime;			//游戏时间
		public int SpawnWaveTime;		//波次间隔
		public int MaxWaveSpawnNum;		//一波最大生成数量
	};
}
