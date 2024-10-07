using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject gameOverPanel;
    private int level;


    public void NextLevel()
    {
        if (level >= levelData.data.Count)
        {
            SceneManager.LoadScene("FinalScene");
            AudioManager.Instance.PlayMusic(AudioManager.Instance.endMusic);
            return;
        }
        Player.Instance.transform.position = levelData.data[level].nextPlayerPos;
        mainCamera.transform.DOMove(levelData.data[level].levelTransform, 1f);
        level++;
    }
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
