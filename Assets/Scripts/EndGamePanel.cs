using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{

    [SerializeReference] private TMPro.TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString();
    }

}
