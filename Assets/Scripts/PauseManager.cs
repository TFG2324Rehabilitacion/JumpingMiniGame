using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public void PauseButton()
    {
        //GameManager.Instance.enPausa = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0;

    }

    public void PlayButton()
    {
        //GameManager.Instance.enPausa = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    public void Exit()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
