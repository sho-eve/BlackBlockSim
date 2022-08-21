using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public int levelCheat = 0;
    public GameObject serverNode;

    // Start is called before the first frame update
    void Start()
    {
        FixLevelCheat();
        serverNode = GameObject.FindWithTag("Server");
        var lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.sortingLayerName = "Effect";
        lineRenderer.sortingOrder = 1;
        var linePositions = new Vector3[] { this.transform.position, serverNode.transform.position };
        lineRenderer.SetPositions(linePositions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if value above 1 or below 0, set value 1 or 0
    void FixLevelCheat() {
        if (levelCheat > 1) {
            levelCheat = 1;
        } else if (levelCheat < 0) {
            levelCheat = 0;
        }
    }
}
