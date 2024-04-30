using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public AudioSource menuMusic;
    public static float songVolume = 1;
    public static float noteSpeed = 4;
    public static bool menuMusicStarted = false;
    void Start()
    {

        string dataPath = Application.dataPath;
        if (Directory.Exists(dataPath + "/MP3Folder"))
        {

        }
        else
        {
            Directory.CreateDirectory(dataPath + "/MP3Folder");
        }
        if (Directory.Exists(dataPath + "/MapFolder"))
        {

        }
        else
        {
            Directory.CreateDirectory(dataPath + "/MapFolder");
        }
        menuMusic.volume = songVolume;
        songVolume = menuMusic.volume;
        if (menuMusicStarted == false)
        {
            menuMusicStarted = true;
            //menuMusic.Play();
        }
    }

    public void OnPlayButton()
    {
        Destroy(menuMusic);
        SceneManager.LoadScene(1);
    }
    public void OnCreateButton()
    {
        Destroy(menuMusic);
        SceneManager.LoadScene(2);
    }
    public void OnOptionsButton()
    {
        SceneManager.LoadScene(3);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
