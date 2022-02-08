using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenState : MonoBehaviour
{
    public List<AudioClip> angryClips;

    public bool isInfected;
    public bool isSafe;
    public bool inHouse;
    public bool isMasked;
    public HouseScript home;
    [HideInInspector]
    public int meetingSpot = -1;
    [HideInInspector]
    public int meetingPoint = -1;
    [HideInInspector]
    public bool isBusy;
    [HideInInspector]
    public TownManager townManager;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public CitizenMaskAssigner maskAssigner;

    public float whackTimeOutIncrement = 0.1f;
    public float whackTimeOut = 0.1f;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        townManager = GameObject.FindGameObjectWithTag("TownManager").GetComponent<TownManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        maskAssigner = GetComponent<CitizenMaskAssigner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Whack()
    {
        if (Time.time > townManager.infectedHitTimeOut)
        {
            ApplyWhack();
        }
        else
        {
            if (isInfected)
            {
                ApplyWhack();
            }
        }

    }

    public void ApplyWhack()
    {
        if (Time.time > whackTimeOut)
        {
            if (!isInfected)
            {
                gameManager.IncreaseAbuse();
            }
            else
            {
                isInfected = false;
            }

            int index = Random.Range(0, angryClips.Count);
            audioSource.clip = angryClips[index];
            audioSource.Play();

            if (meetingPoint >= 0 && meetingSpot >= 0)
            {
                townManager.GetMeetingPoint(meetingPoint).FreeSpot(meetingSpot);
                meetingPoint = -1;
                meetingSpot = -1;
            }
            GetComponent<Animator>().SetTrigger("Whacked");
            whackTimeOut = Time.time + whackTimeOutIncrement;
        }
    }

    public void SetNotMasked()
    {
        isMasked = false;
        maskAssigner.SetNonMasked();
    }

    public void SetMasked()
    {
        isMasked = true;
        maskAssigner.SetMasked();
    }
}
