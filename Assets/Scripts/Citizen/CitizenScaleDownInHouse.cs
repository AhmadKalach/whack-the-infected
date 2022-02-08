using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CitizenScaleDownInHouse : MonoBehaviour
{

    public float shrinkAndStretchDuration = 0.2f;
    public float shrinkScale = 0.1f;
    Vector3 shrinkGoal;
    Vector3 initialScale;


    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        shrinkGoal = new Vector3(shrinkScale, shrinkScale, shrinkScale);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("House"))
        {
            transform.DOScale(shrinkGoal, shrinkAndStretchDuration);
            GetComponent<CitizenState>().inHouse = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("House"))
        {
            transform.DOScale(initialScale, shrinkAndStretchDuration);
            GetComponent<CitizenState>().inHouse = false;
        }
    }
}
