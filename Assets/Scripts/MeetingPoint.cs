using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingPoint : MonoBehaviour
{
    public int maxCitizensAllowed;
    public List<Transform> spots;
    public List<CitizenState> citizens;
    public float warningTime = 3f;

    float nextWarning = 3f;


    public bool[] isAllocatedSpots;
    int nbOfAllocated;

    // Start is called before the first frame update
    void Start()
    {
        isAllocatedSpots = new bool[spots.Count];
        citizens.Capacity = spots.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (citizens.Count > 0)
        {
            InfectUtils.MarkAsUnsafe(citizens);
            if (Time.time > nextWarning)
            {
                InfectUtils.InfectIfOneIsInfected(citizens);
                nextWarning = Time.time + warningTime;
            }
        }
        else
        {
            InfectUtils.MarkAsSafe(citizens);
            nextWarning = Time.time + warningTime;
        }
    }

    public void AddCitizen(CitizenState citizen)
    {
        citizens.Add(citizen);
    }

    public void RemoveCitizen(CitizenState citizen)
    {
        citizens.Remove(citizen);
    }

    public int GetNbOfAllocatedSpots() {
        int counter = 0;
        for (int i = 0; i < isAllocatedSpots.Length; i++) {
            if (isAllocatedSpots[i]) {
                counter++;
            }
        }
        return counter;
    }

    //returns the spot allocated to the player and allocates it, call FreeSpot to free it after
    public int AllocateSpot()
    {
        int pos = -1;
        for (int i = 0; i < isAllocatedSpots.Length; i++)
        {
            if (!isAllocatedSpots[i])
            {
                pos = i;
            }
        }
        if (pos != -1)
        {
            isAllocatedSpots[pos] = true;
        }
        return pos;
    }

    public void FreeSpot(int pos)
    {
        if (pos != -1)
        {
            isAllocatedSpots[pos] = false;
        }
    }

    public bool isFull()
    {
        int k = AllocateSpot();
        if (k == -1)
        {
            return true;
        }
        else
        {
            FreeSpot(k);
            return false;
        }
    }
}
