using System;
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
    public string PlayerId { get; set; }
    public int numJumps { get; set; } = 0;
    public int maxJumps { get; private set; } = 0;
    public float angle { get; private set; } = 0f;

    private int maxRounds;
    private int rounds = 0;
    #endregion

    #region GAME STATE VALUES
    private int _score;

    public bool playerAnim { get; set; }
    #endregion

    #region GAME FLOW VARIABLES
    public bool clientConnected { get; set; }
    public bool playing { get; private set; }

    public bool enPausa = false;
    public float gameStartTime { get; set; }
    #endregion


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            playing = false;
            playerAnim = false;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {

    }

    public void AddPoints(int numPoints)
    {
        _score += numPoints;
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

    public void StartRound()
    {
        numJumps = 0;
        UIManager.Instance.UpdateRoundsText();
        playing = true;
    }

    public void EndRound()
    {
        rounds++;
        playing = false;
        if (rounds < maxRounds)
        {
            numJumps = 0;
            Invoke("ResetUI", 2.0f);
        }
        else
        {
            Invoke("EndGame", 2.0f);
        }
    }

    private void ResetUI()
    {
        UIManager.Instance.StartCountdown(2f);
    }

    public bool RoundFinished()
    {
        return numJumps >= maxJumps;
    }

    public bool GameFinished()
    {
        return rounds >= maxRounds;
    }

    public void StopGame()
    {
        clientConnected = false;
        playing = false;
    }

    private void EndGame()
    {
        UIManager.Instance.ShowEndGamePanel();
        playing = false;
        string dateTime = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH:mm");

        Debug.Log("FIN DEL JUEGO");
        // save data to database
        RealmController.Instance.SetScore(PlayerId, _score);
        RealmController.Instance.SetGameTime(PlayerId, Time.time - gameStartTime);
        RealmController.Instance.SetDateTime(PlayerId, dateTime);
        RealmController.Instance.SetGameCompleted(PlayerId, true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadConfig()
    {
        maxJumps = RealmController.Instance.GetRepsForPlayer(PlayerId);
        maxRounds = RealmController.Instance.GetSeriesForPlayer(PlayerId);
        angle = RealmController.Instance.GetAngleForPlayer(PlayerId);
    }
}
