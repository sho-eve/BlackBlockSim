using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour {

	public float updateTime = 1f;
	public GameObject [] nodesList;

	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void CreateNodeList () {
		nodesList = GameObject.FindGameObjectsWithTag ("Node");
	}
}
