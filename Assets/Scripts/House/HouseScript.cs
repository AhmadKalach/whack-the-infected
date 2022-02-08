using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    public List<AudioClip> screamingClips;
    public int maxNonResidents;
    //citizens isnt useful, just here to make sure the count of peeps inside is correct
    public List<CitizenState> citizens;
    public List<CitizenState> nonResidents;

    public float warningTime = 3f;

    float nextWarning = 3f;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    public void PlaySoundEffect()
    {
        audioSource.clip = screamingClips[Random.Range(0, screamingClips.Count)];
        audioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Citizen"))
        {
            CitizenState citizen = collision.gameObject.GetComponent<CitizenState>();
            citizens.Add(citizen);
            if (citizen.home != this)
            {
                nonResidents.Add(citizen);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Citizen"))
        {
            CitizenState citizen = collision.gameObject.GetComponent<CitizenState>();
            citizens.Remove(citizen);
            if (nonResidents.Contains(citizen))
            {
                nonResidents.Remove(citizen);
            }
        }
    }
}
