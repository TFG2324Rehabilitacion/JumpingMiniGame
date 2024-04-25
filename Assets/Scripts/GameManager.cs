using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    #endregion

    #region GAME CONFIG VALUES
    public int numJumps { get; private set; } = 0;
    public int maxJumps { get; private set; } = 0;

    private int maxRounds;
    private int rounds = 0;
    #endregion

    #region GAME STATE VALUES
    private int _score;

    public bool playerAnim { get; set; }
    public bool playerJumping = false;
    #endregion

    #region GAME FLOW VARIABLES
    public bool clientConnected { get; set; }
    public bool playing { get; private set; }

    public bool enPausa = false;
    #endregion


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            playing = false;
            playerAnim = false;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //maxRounds = ConfigData.Instance().totalSeries;
        //maxJumps = ConfigData.Instance().totalReps;
        maxRounds = 2;
        maxJumps = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPoints(int numPoints)
    {
        _score += numPoints;
        GameManager.Instance.numJumps++;
        if (_score < 0)
        {
            _score = 0;
        }

        UIManager.Instance.UpdateScore();
    }

    public int GetScore()
    {
        return _score;
    }

    public int GetRoundsLeft()
    {
        return maxRounds - rounds;
    }

    private void JumpAnimation()
    {
        numJumps++;
    }

    public void StartRound()
    {
        numJumps = 0;
        UIManager.Instance.ShowJumpsLeft();
        UIManager.Instance.UpdateRoundsText();
        playing = true;
    }

    public void EndRound()
    {
        rounds++;
        playing = false;
        if (rounds < maxRounds)
        {
            Invoke("ResetUI", 2.0f);
        }
        else
        {
            EndGame();
        }
    }

    private void ResetUI()
    {
        UIManager.Instance.StartCountdown(2f);
        UIManager.Instance.ClearJumpFeedback();
    }

    public bool RoundFinished()
    {
        return numJumps >= maxJumps;
    }

    public void StopGame()
    {
        clientConnected = false;
        playing = false;
    }

    private void EndGame()
    {
        UIManager.Instance.EnableConnectionPanel();
        playing = false;
        Debug.Log("FIN DEL JUEGO");
        PlayerData.Instance().totalScore = _score;
        PlayerData.Instance().gameTime = Time.time;
        PlayerData.Instance().SaveData();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
