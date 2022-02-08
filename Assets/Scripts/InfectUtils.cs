using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectUtils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void MarkAsUnsafe(List<CitizenState> citizens)
    {
        foreach (CitizenState citizen in citizens)
        {
            citizen.isSafe = false;
        }
    }

    public static void MarkAsSafe(List<CitizenState> citizens)
    {
        foreach (CitizenState citizen in citizens)
        {
            citizen.isSafe = true;
        }
    }

    public static void InfectIfOneIsInfected(List<CitizenState> citizens)
    {
        bool infected = false;

        foreach (CitizenState citizen in citizens)
        {
            if (citizen.isInfected)
            {
                if (!citizen.isMasked)
                {
                    infected = true;
                }
            }
        }

        if (infected)
        {
            foreach (CitizenState citizen in citizens)
            {
                if (!citizen.isMasked)
                {
                    citizen.isInfected = true;
                }
            }
        }
    }
}
