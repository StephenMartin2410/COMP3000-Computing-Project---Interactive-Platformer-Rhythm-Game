using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public AudioSource menuMusic; //needed variables for function
    public static float songVolume = 1;
    public static float noteSpeed = 5000;
    public static bool menuMusicStarted = false;
    void Start()
    {
        // the start function in this class is used to create the folders which will hold the maps for the songs and the mp3s to play the songs
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
        menuMusic.volume = songVolume; //this chunk is used to tell the program whether or not the menumusic is playing
        songVolume = menuMusic.volume;
        if (menuMusicStarted == false)
        {
            menuMusicStarted = true;
            //menuMusic.Play();
        }
    }

    //this list of functions sends the user to the coressponding menu when the press the related button on the menu screen

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
        Application.Quit();//quits the application when the quit button is pressed
    }
}
