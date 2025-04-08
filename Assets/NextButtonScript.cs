using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
public class NextButtonScript : MonoBehaviour
{
    private InkManager _inkmanager;

    // Start is called before the first frame update
    void Start()
    {
        _inkmanager = FindObjectOfType<InkManager>();

        Button button = GetComponent<Button>();
        
        button.onClick.AddListener(OnClick);

        if (_inkmanager == null)
        {
            Debug.LogError("no se encontro ink manager");
        }
    }

    public void OnClick()
    {
        _inkmanager?.DisplayNextLine();
    }
}
