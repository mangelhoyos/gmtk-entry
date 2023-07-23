using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private CanvasGroup fadeInGroup;
    [SerializeField] private NPCSpawnManager spawnManager;
    [SerializeField] private Transform protagonistTransform;

    private bool gameOver = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EnemyDead();
    }

    [ContextMenu("Game over")]
    public void GameOver()
    {
        if(!gameOver)
        {
            gameOver = true;
            StartCoroutine(FadeScreenToBlack());
        }
    }

    IEnumerator FadeScreenToBlack()
    {
        Debug.Log("No funciona nada de lo que hace miguel");

        while(fadeInGroup.alpha < 1)
        {
            fadeInGroup.alpha += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }

    public void EnemyDead()
    {
        spawnManager.InitializeNewNPC(protagonistTransform);
    }
}
