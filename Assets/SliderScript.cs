using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public AudioSource song;
    public Slider slider;
    public static bool songPlaying;
    public float songLength;



    public void PlayFile(string fileName)//used to play the selected music in the create scene, loads to song in the same way as the playscript
    {
        songPlaying = false;
        //string path = "D:/Unity Projects/COMP3000 Animal Frets/Assets/MP3Folder/trap-future-bass-royalty-free-music-167020.mp3";
        Debug.Log(Application.dataPath);
        string appPath = Application.dataPath;
        appPath = appPath.Trim(' ');
        fileName = fileName.Trim(' ');
        string path = appPath + "/" + "MP3Folder/" + fileName + ".mp3";
        Debug.Log(path);
        Uri uri = new Uri(path, UriKind.Absolute);
        Debug.Log(uri);

        StartCoroutine(LoadAndPlay(uri));
    }

    IEnumerator LoadAndPlay(Uri uri)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                song.clip = clip;
                //song.Play();
                //songPlaying = true;
                songLength = song.clip.length;
                slider.maxValue = songLength - 1;
                Debug.Log(song.clip.length);
                //song.Play();
            }
        }
    }

    // Start is called before the first frame update
    void Start()//runs the loadandplay coroutine
    {
        PlayFile(CreateDropdownScript.selectedMp3);
        song.volume = MenuScript.songVolume;
        //slider.maxValue = songLength;
        Debug.Log(songLength);
        //song.Play();
    }


    // Update is called once per frame
    void Update()//updates the slider in the create scene to reflect the time of the song
    {

        if (slider.value != songLength)
        {
            slider.value = song.time;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (song.isPlaying)
            {
                Debug.Log(song.time);
                songPlaying = false;
                song.Pause();
            }
            else
            {
                songPlaying = true;
                song.Play();
            }
        }

    }
}
