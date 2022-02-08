using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbuseOMeterScript : MonoBehaviour
{
    public Image imageToFill;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = gameManager.abuse / gameManager.maxAbuse;
        imageToFill.fillAmount = fillAmount;
    }
}
