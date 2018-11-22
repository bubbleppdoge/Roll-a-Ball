using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour {

	public Text speedText;			//小球速度设置
	public Slider speedSlider;

	public Text camSpeedText;		//摄像机跟随速度
	public Slider camSpeedSlider;

	public InputField mapWidth;		//地图大小设置
	public InputField mapLength;

	void Start()					//初始化UI
	{
		speedSlider.value = 4;
		camSpeedSlider.value = 4;
		mapWidth.text = "20";
		mapLength.text = "20";

		mapWidth.onEndEdit.AddListener(delegate {RightInput(mapWidth);}); 	//输入框结束输入监听事件
		mapLength.onEndEdit.AddListener(delegate {RightInput(mapLength);});
	}

	void Update()
	{
		speedText.text = "Move Speed: " + speedSlider.value.ToString();		//滑动条的值和文本显示的关联
		camSpeedText.text = "Camera Follow Speed: " + camSpeedSlider.value.ToString();
	}

	void RightInput(InputField input) 										//输入结束后，保证地图大小在10-40之间
	{
		input.text = int.Parse (input.text) > 40 ? "40" : input.text;
		input.text = int.Parse (input.text) < 10 ? "10" : input.text;
	}

	public void SetGameData()												//保存设置，记录数据
	{
		SettingData.Instance.moveSpeed = (int)speedSlider.value;
		SettingData.Instance.mapWidth = int.Parse (mapWidth.text);
		SettingData.Instance.mapLength = int.Parse (mapLength.text);
		SettingData.Instance.camMoveSpeed = (int)speedSlider.value;
	}
}
