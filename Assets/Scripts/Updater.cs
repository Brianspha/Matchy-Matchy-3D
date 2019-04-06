using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Updater : MonoBehaviour {

    // Use this for initialization
    public Text scoreText;
    public Text leveText;

    public int score;
    public int level=1;
    public float Speed = 4;
    private float speedIncrementor=.35f;
    public int MinforLevelCompletion = 15;
    public int currentLevelScore = 0;
    public bool reset = false;
    public float resetDelay = 8f;
    public float transDelay = 7;
    public float currentrestDelay;
    public GameObject levelCompleteUI,GameOverUI,sceneTransUI;
     Image levelCompleteUIFade,SceneTransition;
    public int Factor = 300;
    public float decreaseFactor = .25f;
    public float GunShot = 5;
    GunController controller;
    public AudioSource main,stageWin;
    public int DeathCount;
    private bool gameOver;
    GameObject updater;
    public float transBetweenScenes = 5;
    bool loadScene = false;
    private bool canTrans;
    int whichScence;//@dev 1,2
    public void Awake()
    {
        stageWin.volume = 0;
        stageWin.Stop();
        SetGameOverVisible(true);

    }
    void Start () {
        SetGameOverVisible(true);
        updater = GameObject.FindGameObjectWithTag("Updater");
        score = 0;
        currentrestDelay = resetDelay;
        levelCompleteUIFade = levelCompleteUI.transform.Find("Panel").gameObject.GetComponent<Image>();
        SceneTransition = sceneTransUI.transform.Find("Panel").gameObject.GetComponent<Image>();
        controller = GameObject.FindGameObjectWithTag("GunParent").GetComponent<GunController>();
        main.Play();
        GameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        updateLevel();
        GameOverYet();
        if (gameOver)
        {
            updater.SetActive(false);
            ShowGameOver();

        }
		if(reset)
        {
            main.volume -= Time.deltaTime;
            stageWin.volume = 1;
                main.Stop();
            currentrestDelay -= Time.deltaTime;
            levelCompleteUIFade.CrossFadeColor(new Color(1f, 1f, 1f, 0f), resetDelay, false, true);
            controller.resetGunshot();
         if (currentrestDelay <= 0)
        {
            currentrestDelay = resetDelay;
            ActivateLeveCompleteUI(false);
                stageWin.Play();
            Reset(false);
                main.volume = 1;
                main.Play();
        }
        }

        Debug.Log("WhichSecene To Load: " + whichScence);
     	}

    private void LoadScene(int whichScence)
    {
        switch (whichScence) {
            case 1:
                SceneManager.LoadScene("StartScene");
                break;
            case 2:
                SceneManager.LoadScene("Main1");
                break;
        }
        SceneTransition.CrossFadeColor(new Color(1f, 1f, 1f, 0f), transDelay, false, true);
        Debug.Log("Called: " + whichScence);
    }

    /// <summary>
    /// updates score using the specified amount
    /// </summary>
    /// <param name="amount"></param>
    public void UpdateScore(int amount)
    {
        if (amount <= 0) return;
        score += amount;
        currentLevelScore += amount;
        scoreText.text = score.ToString();
    }
    public float GetSpeed()
    {
        return Speed;
    }
    public void updateLevel()
    {
        if (currentLevelScore == MinforLevelCompletion)
        {
            ActivateLeveCompleteUI(true);
            level++;
            leveText.text = level.ToString();
            Speed += speedIncrementor;
            currentLevelScore = 0;
            Reset(true);
        }
    }
    private void SetGameOverVisible(bool to)
    {
        GameOverUI.SetActive(to);

    }
    private void Reset(bool to)
    {
        reset = to;
    }
    public void ActivateLeveCompleteUI(bool yesOrNo)
    {
        levelCompleteUI.SetActive(yesOrNo);
    }
    public void IcreamentDeathCount()
    {
        if (DeathCount == 4)
        {
            gameOver = true;
            SetGameOverVisible(false);
            ShowGameOver();
        }
    }
    public void GameOverYet()
    {
        gameOver = DeathCount >= 4;
    }
    private void ShowGameOver()
    {
        GameOverUI.SetActive(true);
        updater.SetActive(false);
    }

    public void MainMenu()
    {
        whichScence = 1;
        //SceneTransition.CrossFadeColor(new Color(1f, 1f, 1f, 0f), transDelay, false, true);
        //GameOverUI.SetActive(false);
        //loadScene = true;
        LoadScene(whichScence);
    }
    public void PlayAgain()
    {
        whichScence = 2;
        //SceneTransition.CrossFadeColor(new Color(1f, 1f, 1f, 0f), transDelay, false, true);
        //GameOverUI.SetActive(false);
        //loadScene = true;
        LoadScene(whichScence);


    }
    public void LeaveGameu()
    {
        SceneTransition.CrossFadeColor(new Color(1f, 1f, 1f, 0f), transDelay, false, true);
        loadScene = true;
            Application.Unload();
        
    }
    public bool GameOver()
    {
        return gameOver;
    }

}
