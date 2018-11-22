using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBtnCtrl : MonoBehaviour {

	public GameObject settingMenu;
	public GameObject rankingListPage;
	public GameObject CHEATSETTING;

	//作弊菜单计数激活
	private int clickCount = 3;
	private int curClick = 0;

	void Start()		//游戏开始前把所有菜单隐藏
	{
		settingMenu.SetActive (false);
		rankingListPage.SetActive (false);
		CHEATSETTING.SetActive (false);
	}

	//菜单四个按钮点击事件
	public void PlayBtn()
	{
		GetComponent<Setting> ().SetGameData ();
		GetComponent<RankingList> ().SetRankData ();
		if (GameObject.Find ("MyGame") != null)
			GameObject.Find ("MyGame").GetComponent<MyGame> ().SetMyData ();

		//加载游戏场景
		SceneManager.LoadScene (1);
	}

	public void SettingBtn()
	{
		settingMenu.SetActive (true);
	}

	public void RankingListBtn()
	{
		rankingListPage.SetActive (true);
	}

	public void ExitBtn()
	{
		Application.Quit ();
	}
		
	public void BackBtn(GameObject obj)		//返回按钮
	{
		obj.SetActive (false);
	}

	/////////////////////////////////////////////////
	/// 作弊菜单
	/////////////////////////////////////////////////
	public void CheatSettingBtn()
	{
		curClick++;
		if (curClick >= clickCount) {
			CHEATSETTING.SetActive (true);
		}
	}
}
