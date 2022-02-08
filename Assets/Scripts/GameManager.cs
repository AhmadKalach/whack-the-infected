using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayButton playButton;
    public float maxAbuse = 100f;
    public float abuseDecayPerSecond = 2f;
    public float abuseIncreaseWhenWhacked = 15f;
    public float gameTimeOutValue = 160f;
    public float abuse;
    [HideInInspector]
    public TownManager townManager;

    float gameEndTime;

    // Start is called before the first frame update
    void Start()
    {
        townManager = GameObject.FindGameObjectWithTag("TownManager").GetComponent<TownManager>();
        gameEndTime = Time.time + gameTimeOutValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (abuse > 0)
        {
            abuse = abuse - (abuseDecayPerSecond * Time.deltaTime);
        }

        if (abuse > 98)
        {
            GameOverFromAbuse();
        }

        if (Time.time > gameEndTime)
        {
            PlayerPrefs.SetInt("InfectedCount", townManager.GetNumberOfInfected());
            GameWon();
        }
    }

    public void IncreaseAbuse()
    {
        abuse = Mathf.Clamp(abuse + abuseIncreaseWhenWhacked, 0, maxAbuse);
    }

    public void GameOverFromAbuse()
    {
        playButton.GoToGameOverAbuse();
    }

    public void GameOverFromInfections()
    {
        playButton.GoToGameOverInfected();
    }

    public void GameWon()
    {
        playButton.GoToGameWon();
    }
}
