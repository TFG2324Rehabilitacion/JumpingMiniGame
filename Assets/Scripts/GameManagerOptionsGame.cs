using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerOptionsGame : MonoBehaviour
{
    public ControllerOptions optionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        optionsPanel = GameObject.FindGameObjectWithTag("Opciones").GetComponent<ControllerOptions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowOptions()
    {
        optionsPanel.OptionsPanel.SetActive(true);
    }
}
