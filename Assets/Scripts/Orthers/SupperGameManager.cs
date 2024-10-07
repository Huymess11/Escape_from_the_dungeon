using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SupperGameManager : MonoBehaviour
{
    public void Replay()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.Instance.PlayMusic(AudioManager.Instance.music);
    }
    public void Home()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        SceneManager.LoadScene("HomeScene");
        AudioManager.Instance.PlayMusic(AudioManager.Instance.music);
    }
    public void Play()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        SceneManager.LoadScene("GameScene");
    }
}
