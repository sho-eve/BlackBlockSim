using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSwitch : MonoBehaviour {
	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void LineViewSwitch () {
		GameObject [] nodes = GameObject.FindGameObjectsWithTag ("Node");
		var checkRenderer = nodes [0].GetComponent<LineRenderer> ();
		if (checkRenderer.enabled == true) {
			foreach (GameObject node in nodes) {
				node.GetComponent<LineRenderer> ().enabled = false;
			}
		} else {
			foreach (GameObject node in nodes) {
				node.GetComponent<LineRenderer> ().enabled = true;
			}
		}
	}
}
