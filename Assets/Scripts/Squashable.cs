using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Squashable : MonoBehaviour
{
    public float squashFinalY = 0.3f;
    public float squashingDuration = 0.2f;
    public float squashTime = 1f;

    public float timeToReturnToNormal;


    bool squashing = false;
    float defaultScaleY;
    TownManager townManager;

    private void Start()
    {
        townManager = GameObject.FindGameObjectWithTag("TownManager").GetComponent<TownManager>();
        defaultScaleY = transform.localScale.y;
    }

    public void Squash()
    {
        if (!squashing)
        {
            squashing = true;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(transform.DOScaleY(defaultScaleY * squashFinalY, squashingDuration).SetEase(Ease.OutElastic));
            sequence.Append(transform.DOScaleY(defaultScaleY * squashFinalY, squashTime));
            sequence.Append(transform.DOScaleY(defaultScaleY, timeToReturnToNormal).SetEase(Ease.OutElastic));

            sequence.OnComplete(() => squashing = false);

            sequence.Play();

            if (GetComponent<CitizenState>() != null)
            {
                GetComponent<CitizenState>().Whack();
                if (!GetComponent<CitizenState>().inHouse)
                {
                    sequence.Play();
                }
            }
            else
            {
                sequence.Play();
            }

            if (GetComponent<HouseScript>() != null)
            {
                HouseScript houseScript = GetComponent<HouseScript>();
                if (houseScript.citizens.Count > 1)
                {
                    if (houseScript.screamingClips.Count > 0)
                    {
                        houseScript.PlaySoundEffect();
                    }
                }

                bool isInfected = false;

                foreach (CitizenState citizen in houseScript.citizens)
                {
                    if (citizen.isInfected)
                    {
                        isInfected = true;
                    }
                }
                if (isInfected)
                {
                    townManager.IncreaseInfectedHitTimeOut();
                }

                houseScript.citizens.ForEach(citizen => citizen.Whack());
            }
        }
    }
}
