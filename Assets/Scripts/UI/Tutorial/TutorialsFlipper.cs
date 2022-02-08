using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialsFlipper : MonoBehaviour
{
    public string nextScene;
    public List<RectTransform> tutorials;
    int counter = 0;
    PlayButton playButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton = GetComponent<PlayButton>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (i == counter)
            {
                tutorials[i].gameObject.SetActive(true);
            }
            else
            {
                tutorials[i].gameObject.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            counter++;
        }

        if (counter >= tutorials.Count || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}
