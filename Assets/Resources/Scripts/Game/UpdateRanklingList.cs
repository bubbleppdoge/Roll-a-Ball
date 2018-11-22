using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Xml;

public class UpdateRanklingList : MonoBehaviour {
	//此脚本用于每次产生一个分数时保存数据到xml中，大致和菜单界面的脚本功能相同
	public Text rankText;
	public Text nameText;
	public Text scoreText;
	private Dictionary<string, int> rankDict = new Dictionary<string, int>();

	private int NumOfRank = 10;																			//xml中保存数据的数量

	void Start()
	{
		rankDict = SettingData.Instance.rankDict;														//读取菜单中保存过来的排行榜数据
		rankText.text = "Rank";
		nameText.text = "Name";
		scoreText.text = "Score";
	}

	public void SetSimpleItem(string newName, int newScore)												//设置单个数据，一般游戏结束后保存单局成绩
	{
		rankDict [newName] = newScore;																	//把结果存入字典中
		rankDict = rankDict.OrderByDescending (p => p.Value).ToDictionary (p => p.Key, o => o.Value);	//对字典降序排序

		//如果存在xml，则更改xml中的数据，不存在就新建xml，保存数据
		string path = Application.dataPath + "/Resources/RankingList.xml";
		if (File.Exists (path))
			UpdateXML (path, newName, newScore);
		else
			CreateXML (path, newName, newScore);
	}

	void CreateXML(string path, string newName, int newScore)
	{
		XmlDocument xml = new XmlDocument ();
		XmlElement root = xml.CreateElement ("RankingList");
		XmlElement item = xml.CreateElement ("Item");
		XmlElement name = xml.CreateElement ("Name");
		XmlElement score = xml.CreateElement ("Score");
		name.InnerText = newName;		//保存姓名和分数
		score.InnerText = newScore.ToString ();
		item.AppendChild (name);
		item.AppendChild (score);
		root.AppendChild (item);
		xml.AppendChild (root);
		xml.Save (path);
	}

	void UpdateXML(string path, string newName, int newScore)
	{
		XmlDocument xml = new XmlDocument ();
		xml.Load (path);
		XmlNode root = xml.SelectSingleNode ("RankingList");
		XmlNodeList items = root.SelectNodes ("Item");
		XmlNode insertNode = null;

		//根据分数降序排行
		foreach (XmlNode item in items) {
			XmlNode mScore = item.FirstChild.NextSibling;
			if (newScore > int.Parse (mScore.InnerText)) {
				insertNode = item;
				break;
			}
		}

		//分数在前10名则保存
		if (insertNode != null || items.Count < NumOfRank) {
			XmlElement item = xml.CreateElement ("Item");
			XmlElement name = xml.CreateElement ("Name");
			XmlElement score = xml.CreateElement ("Score");
			name.InnerText = newName;
			score.InnerText = newScore.ToString ();
			item.AppendChild (name);
			item.AppendChild (score);
			if (insertNode != null)
				root.InsertBefore (item, insertNode);
			else
				root.AppendChild (item);
			xml.AppendChild (root);
			if (items.Count > NumOfRank)
				root.RemoveChild (items [NumOfRank]);
			xml.Save (path);
		}
	}

	public void ShowRankingList()		//显示排行榜
	{
		if (rankDict.Count != 0) {
			int i = 1;
			foreach (KeyValuePair<string, int> item in rankDict) {
				rankText.text += "\n" + i.ToString ();
				nameText.text += "\n" + item.Key;
				scoreText.text += "\n" + item.Value.ToString ();
				i++;
			}
		} else {
			rankText.text += "\nnull";
			nameText.text += "\nnull";
			scoreText.text += "\nnull";
		}
	}
}
