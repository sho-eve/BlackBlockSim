using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour, IPointerClickHandler {

	public int levelCheat = 0;
	public GameObject serverNode;
	GameObject childPanel;

	// Start is called before the first frame update
	void Start () {
		serverNode = GameObject.FindWithTag ("Server");
		childPanel = transform.Find ("Panel").gameObject;
		childPanel.SetActive (false);
		FixLevelCheat ();
		RenderLineThisTo (serverNode);
	}

	// Update is called once per frame
	void Update () {

	}

	//if value above 1 or below 0, set value 1 or 0
	void FixLevelCheat () {
		if (levelCheat > 1) {
			levelCheat = 1;
		} else if (levelCheat < 0) {
			levelCheat = 0;
		}
	}

	void RenderLineThisTo (GameObject node) {
		var lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.sortingLayerName = "Line";
		lineRenderer.sortingOrder = 0;
		lineRenderer.startWidth = 0.025f;
		lineRenderer.endWidth = 0.025f;
		var linePositions = new Vector3 [] { node.transform.position, transform.position };
		lineRenderer.SetPositions (linePositions);
	}

	public void OnPointerClick (PointerEventData eventData) {
		if (childPanel.activeSelf == true) {
			childPanel.SetActive (false);
		} else {
			childPanel.SetActive (true);
		}
	}

}
