using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    public int citizensToInitiallyInfect;
    public List<CitizenState> citizens;
    public List<HouseScript> houses;
    public MeetingPoint[] meetingPoints;
    public HouseScript groceryStore;
    public float infectedHitTimeOutIncrease = 0.1f;
    [HideInInspector]
    public float infectedHitTimeOut;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        HashSet<int> alreadyInfected = new HashSet<int>();
        while (alreadyInfected.Count < citizensToInitiallyInfect)
        {
            int citizenToInfect = Random.Range(0, citizens.Count);
            citizens[citizenToInfect].isInfected = true;
            alreadyInfected.Add(citizenToInfect);
        }

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (citizens.Count == GetNumberOfInfected())
        {
            gameManager.GameOverFromInfections();
        }

        if(GetNumberOfInfected() == 0)
        {
            PlayerPrefs.SetInt("InfectedCount", 0);
            gameManager.GameWon();
        }
    }

    public int GetNumberOfInfected()
    {
        int infected = 0;
        foreach (CitizenState citizen in citizens)
        {
            if (citizen.isInfected) {
                infected++;
            }
        }
        return infected;
    }

    public void IncreaseInfectedHitTimeOut()
    {
        infectedHitTimeOut = Time.time + infectedHitTimeOutIncrease;
    }

    public MeetingPoint GetMeetingPoint(int i)
    {
        return meetingPoints[i];
    }

    public HouseScript GetGroceryStore()
    {
        return groceryStore;
    }
}
