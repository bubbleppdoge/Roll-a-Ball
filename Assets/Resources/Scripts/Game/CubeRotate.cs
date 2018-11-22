using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour {

	//黄色方块自转
	void Update () {
		transform.eulerAngles += new Vector3(0, 45, 0) * Time.deltaTime;
	}
}
