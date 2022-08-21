using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputNumberOfNode : MonoBehaviour
{

    public InputField _inputNumberOfNode;
    public GameObject _blockPrefab;
    public GameObject _canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateNode() {
        for(int i = 0; i < float.Parse(_inputNumberOfNode.text); i++) {
            GameObject block = Instantiate(_blockPrefab);
            block.transform.SetParent(_canvas.transform, false);
            block.transform.position = new Vector3(Random.Range(0, 1490), Random.Range(0, 780), 0);
        }
    }

    public void GenerateButton() {
        GenerateNode();
    }
}