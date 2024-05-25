using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject CaiDat;
    public GameObject Menu;
    public GameObject HuongDan;
    public void Awake()
    {
        CaiDat.SetActive(false);
        Menu.SetActive(true);
        HuongDan.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
    public void Setting()
    {
        CaiDat.SetActive(true);
        Menu.SetActive(false);
    }
    public void TroLai()
    {
        CaiDat.SetActive(false);
        HuongDan.SetActive(false);
        Menu.SetActive(true);
    }
    public void huongDan()
    {
        HuongDan.SetActive(true);
        Menu.SetActive(false);
    }
    public void ChoiTiep()
    {
        SaveGame.LoadSavedScene();
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
