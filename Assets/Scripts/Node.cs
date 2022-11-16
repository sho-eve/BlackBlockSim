using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class Node : MonoBehaviour, IPointerClickHandler {

	public float levelCheat = 0;
	public GameObject serverNode;
	public GameObject serverScript;
	GameObject childPanel;
	public GameObject [] simNodes;
	Vector3 [] linePositions;

	bool simAct = false;

	float seconds = 0f;

	Queue<GameObject> consensusQueue = new Queue<GameObject> ();
	Queue<bool> boolQueue = new Queue<bool> ();

	public int numberOfStackToAble = 10;

	bool [] simNodesJudge;

	Slider cheatSlider;

	//マイニングの閾値
	public float miningState;
	float percentageMiningState = 100;

	// Start is called before the first frame update
	void Start () {
		serverNode = GameObject.FindWithTag ("Server");
		serverScript = GameObject.FindWithTag ("ServerScript");
		childPanel = transform.Find ("Panel").gameObject;
		childPanel.SetActive (true);
		FixLevelCheat ();
		RenderLineThisTo (serverNode);
		cheatSlider = GetComponentInChildren<Slider> ();
	}

	// Update is called once per frame
	void Update () {
		levelCheat = cheatSlider.value;
		seconds += Time.deltaTime;
		if (seconds >= serverScript.GetComponent<Server> ().updateTime) {
			if (simAct == true) {
				int nodeNum = Random.Range (0, simNodes.Length - 1);

				if (simNodesJudge [nodeNum] == true) {
					RenderLineThisTo (simNodes [nodeNum]);
					enqueueAndDequeue (simNodes [nodeNum]);
				} else {
					print ("can't connect");
				}



				if (childPanel.activeSelf == true) {
					//print (simNodes [nodeNum].transform.position);
					//print (consensusQueue.Peek ().transform.position);
				}

			}
			seconds = 0f;
		}

		if (consensusQueue.Count () > 0) {
			if (mining (consensusQueue.Peek ()) == 1) {
				broadcastList ();
				finishMiningObj ();
			}
		}

	}

	//if value above 1 or below 0, set value 1 or 0
	void FixLevelCheat () {
		if (levelCheat > 1) {
			levelCheat = 1;
		} else if (levelCheat < 0) {
			levelCheat = 0;
		}
	}

	//render line this to "node"
	void RenderLineThisTo (GameObject node) {
		var lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.sortingLayerName = "Line";
		lineRenderer.sortingOrder = 0;
		lineRenderer.startWidth = 0.025f;
		lineRenderer.endWidth = 0.025f;
		linePositions = new Vector3 [] { node.transform.position, transform.position };
		lineRenderer.SetPositions (linePositions);
	}

	//slider panel set active 
	public void OnPointerClick (PointerEventData eventData) {
		if (childPanel.activeSelf == true) {
			childPanel.SetActive (false);
		} else {
			childPanel.SetActive (true);
		}
	}

	//ready to start simulate
	public void Sim () {
		//simNodes = GameObject.FindGameObjectsWithTag ("Node");
		//System.Array.Resize (ref simNodesJudge, simNodes.Length);

		/*
		for (int i = 0; i < cam.GetComponent<InputNumberOfNode> ().simNodesList.Length; i++) {
			simNodes [i] = cam.GetComponent<InputNumberOfNode> ().simNodesList [i];
		}
		*/

		simNodes = GameObject.FindGameObjectWithTag ("Server").GetComponent<Server> ().nodesList;

		simNodesJudge = new bool [simNodes.Length];

		for (int i = 0; i < simNodes.Length; i++) {
			simNodesJudge [i] = true;
		}
		simAct = true;
		miningState = simNodes.Length / percentageMiningState;
	}

	void enqueueAndDequeue (GameObject obj) {
		consensusQueue.Enqueue (obj);
		if (consensusQueue.Count > numberOfStackToAble) {
			consensusQueue.Dequeue ();
		}

		/*
		float judgeCheat = Random.Range (0f, 1f);

		if (obj.GetComponent<Node> ().levelCheat >= judgeCheat) {
			//false mean cheat
			boolQueue.Enqueue (false);
		} else {
			//true don't mean cheat
			boolQueue.Enqueue (true);
		}

		if (boolQueue.Count > numberOfStackToAble) {
			boolQueue.Dequeue ();
		}
		*/
	}

	int mining (GameObject obj) {
		float hashValue = UnityEngine.Random.Range (0f, (float)simNodes.Length);
		if (hashValue <= miningState) {
			float judgeCheat = Random.Range (0f, 1f);
			if (obj.GetComponent<Node> ().levelCheat >= judgeCheat) {
				for (int i = 0; i < simNodes.Length; i++) {
					if (simNodes [i] == obj) {
						simNodesJudge [i] = false;
					}
				}
			} else {
				for (int i = 0; i < simNodes.Length; i++) {
					if (simNodes [i] == obj) {
						simNodesJudge [i] = true;
					}
				}
			}
			return 1;
		} else {
			return 0;
		}
	}

	void broadcastList () {
		for (int i = 0; i < simNodes.Length; i++) {
			simNodes [i].GetComponent<Node> ().simNodesJudge = simNodesJudge;
		}
	}

	void finishMiningObj () {
		consensusQueue.Dequeue ();
	}


}
