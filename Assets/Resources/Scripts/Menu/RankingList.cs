using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;
using System.Linq;

public class RankingList : MonoBehaviour {

	public Text rankText;	//名次文本
	public Text nameText;	//姓名文本
	public Text scoreText;	//分数文本

	private Dictionary<string, int> rankDict = new Dictionary<string, int>();

	void Start()
	{
		rankText.text = "Rank";
		nameText.text = "Name";
		scoreText.text = "Score";

		//查询排行榜本地数据，如果存在就加载（游戏未发布文本保存在Resources文件夹，发布后保存在游戏放置文件夹下Resources文件夹中）
		string path = Application.dataPath + "/Resources/RankingList.xml";
		if (File.Exists (path))
			LoadXML (path);
		
		ShowRankingList ();		//显示排行榜
	}

	void LoadXML(string path)	//XML加载
	{
		XmlDocument xml = new XmlDocument ();
		xml.Load (path);
		XmlNodeList items = xml.SelectNodes ("//RankingList/Item");
		foreach (XmlNode item in items) {
			XmlNode name = item.FirstChild;
			XmlNode score = name.NextSibling;
			rankDict [name.InnerText] = int.Parse (score.InnerText);	//把数据存储到字典中，便于下个场景排行榜数据的加载
		}
	}

	void ShowRankingList()
	{
		//有本地数据
		if (rankDict.Count != 0) {
			int i = 1;
			foreach (KeyValuePair<string, int> item in rankDict) {
				rankText.text += "\n" + i.ToString ();
				nameText.text += "\n" + item.Key;
				scoreText.text += "\n" + item.Value.ToString ();
				i++;
			}
		} else {
			//无本地数据
			rankText.text += "\nnull";
			nameText.text += "\nnull";
			scoreText.text += "\nnull";
		}
	}
		
	public void SetRankData()	//保存字典到下个场景
	{
		SettingData.Instance.rankDict = rankDict;
	}
}