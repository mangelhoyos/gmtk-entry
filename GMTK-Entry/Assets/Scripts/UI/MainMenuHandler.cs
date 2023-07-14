using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private GameObject initialCanvas;
    private bool initialized = false;

    private void Update()
    {
        if(!initialized)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                director.gameObject.SetActive(true);
                initialCanvas.SetActive(false);
                director.Play();
                director.stopped += ChangeScene;
            }
        }
    }

    private void ChangeScene(PlayableDirector aDirector)
    {
        SceneManager.LoadScene("Main");
    }

}
