using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputNumberOfNode : MonoBehaviour {

	public InputField _inputNumberOfNode;
	public GameObject _blockPrefab;
	public GameObject _canvas;

	bool clickCheck = false;

	public GameObject [] simNodesList;

	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void GenerateNode () {
		for (int i = 0; i < float.Parse (_inputNumberOfNode.text); i++) {
			GameObject block = Instantiate (_blockPrefab);
			block.name = ("Client Node " + (i + 1));
			block.transform.SetParent (_canvas.transform, false);
			block.transform.localPosition = new Vector3 (Random.Range (-960, 960), Random.Range (-540, 540), 0);
		}
		clickCheck = true;
	}

	public void GenerateButton () {
		GenerateNode ();
	}

	public void StartButton () {
		if (clickCheck == true) {
			SimStart ();
		} else {
			print ("Please input number of node");
		}
	}

	void SimStart () {
		simNodesList = GameObject.FindGameObjectsWithTag ("Node");
		GameObject.FindGameObjectWithTag ("Server").GetComponent<Server> ().CreateNodeList ();
		foreach (GameObject node in simNodesList) {
			node.GetComponent<Node> ().Sim ();
		}
		GameObject.FindGameObjectWithTag ("Server").GetComponent<ListControl> ().getNodeList ();
	}
}
