using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListControl : MonoBehaviour {

	GameObject [] simNodes;
	public Text text;
	bool simCom = false;

	public bool [] simNodesJudge;

	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (simCom == true) {
			updateList ();

			//print ("Debug");
		}
		text.transform.position -= new Vector3 (0, Input.GetAxis ("Mouse ScrollWheel") * 100, 0);
	}

	public void getNodeList () {
		simNodes = GameObject.FindGameObjectWithTag ("Server").GetComponent<Server> ().nodesList;
		simCom = true;
		simNodesJudge = new bool [simNodes.Length];
	}

	void updateList () {
		text.text = "Node name, Cheat level, Rate of connecting, GenerateBlock, Listin\n";
		foreach (GameObject node in simNodes) {
			text.text += node.name.ToString () + ", " + node.GetComponent<Node> ().levelCheat.ToString () + ", " + node.GetComponent<Node> ().rateOfConnect.ToString () + ", " + node.GetComponent<Node> ().numberOfGeneratingBlock.ToString () + "\n";
		}
		text.GetComponent<RectTransform> ().sizeDelta = new Vector2 (1500, (simNodes.Length + 1) * 80);

		text.text += "\nBlacklist\n";

		for (int i = 0; i < simNodes.Length; i++) {
			if (simNodesJudge.Length > 0) {
				if (simNodesJudge [i] == false) {
					text.text += simNodes [i].name + "\n";
				}
			}

		}
	}
}
