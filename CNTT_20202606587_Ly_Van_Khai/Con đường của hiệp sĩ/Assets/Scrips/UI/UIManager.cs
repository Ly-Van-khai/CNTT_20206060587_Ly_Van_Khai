using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Thua")]
    public GameObject overScreen;
    public AudioClip overSound;
    [Header("Tam Dung")]
    public GameObject pauseScreen;
    public void Awake()
    {
        overScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TamDung(!pauseScreen.activeInHierarchy);
        }
    }
    public void TamDung(bool TrangThai)
    {
        pauseScreen.SetActive(TrangThai);
        if (TrangThai)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void Thua()
    {
        overScreen.SetActive(true);
        SoundManager.instance.PhatNhac(overSound);
    }
    public void ChoiLai()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Thoat()
    {
        // Thoát khỏi trò chơi
        Application.Quit();

    }
    public void LuuVaThoat()
    {
        SaveGame.SaveGameState();
        // Thoát khỏi trò chơi
        Application.Quit();

    }
    public void TiengGame()
    {
        SoundManager.instance.changeTiengGame(0.2f);
    }
    public void AmThanh()
    {
        SoundManager.instance.changeAmThanh(0.2f);
    }
}
