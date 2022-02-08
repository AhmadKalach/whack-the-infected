using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfectedCountScript : MonoBehaviour
{
    TownManager townManager;
    TextMeshProUGUI countText;
    int prevCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        townManager = GameObject.FindGameObjectWithTag("TownManager").GetComponent<TownManager>();
        countText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int count = townManager.GetNumberOfInfected();
        countText.SetText("" + count + "/" + townManager.citizens.Count);
    }
}
