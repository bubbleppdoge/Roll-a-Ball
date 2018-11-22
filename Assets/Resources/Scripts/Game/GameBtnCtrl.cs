using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBtnCtrl : MonoBehaviour {

	public GameObject gameOverPage;			//游戏结束页
	public InputField nameInput;			//游戏结束页名字输入框

	public GameObject rankingListPage;		//排行榜页

	public GameObject pausePage;			//暂停页面
	public Text camSpeedText;				//摄像机跟随速度
	public Slider camSpeedSlider;

	private UpdateRanklingList upRank;
	private GameController gameCtrl;

	void Start()							//初始化
	{
		gameOverPage.SetActive (false);
		rankingListPage.SetActive (false);
		pausePage.SetActive (false);

		camSpeedSlider.value = SettingData.Instance.camMoveSpeed;

		upRank = gameOverPage.GetComponent<UpdateRanklingList> ();
		gameCtrl = GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	void Update()
	{
		camSpeedText.text = "Camera Follow Speed: " + camSpeedSlider.value.ToString();		//暂停界面文字和滑动条关联
	}

	public void SubmitBtn()				//结束界面提交按钮
	{
		upRank.SetSimpleItem (nameInput.text, gameCtrl.playerScore);
		rankingListPage.SetActive (true);
		upRank.ShowRankingList ();
	}

	public void PlayAgainBtn()			//排行榜界面重玩按钮
	{
		SceneManager.LoadScene (1);
	}

	public void BackToMenu()			//返回菜单按钮（各个界面）
	{
		SceneManager.LoadScene (0);
	}

	public void PauseBtn()				//游戏界面暂停按钮
	{
		Time.timeScale = 0;				//游戏暂停
		pausePage.SetActive (true);
	}

	public void ContinueBtn()			//暂停界面继续按钮
	{
		Time.timeScale = 1;
		pausePage.SetActive (false);
	}

	public void SliderEvent()			//暂停界面改变摄像机跟随速度事件
	{
		Camera.main.GetComponent<CameraFollow>().SetFollowSpeed((int)camSpeedSlider.value);
	}
}
