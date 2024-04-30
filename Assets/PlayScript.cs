using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text comboText;
    public TMP_Text duration;
    public static int score;
    public static int combo;
    public float songDuration;
    public string soundPath;
    public bool playing = false;
    public GameObject Note;
    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;
    public GameObject Chord;
    public GameObject Chord1;
    public GameObject Chord2;
    public GameObject Chord3;
    public AudioSource songData;
    float[] distance1 = { };
    float[] distance2 = { };
    float[] distance3 = { };
    float[] distance4 = { };
    // Start is called before the first frame update

    public void PlayFile(string fileName)
    {
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
                songData.clip = clip;
                songDuration = songData.clip.length;
                songData.Play();
            }
        }
    }

    void Start()
    {
        //Debug.Log(PlayDropdownScript.selectedMap);
        //songData.Play();
        SliderScript.songPlaying = false;
        Destroy(GameObject.Find("Lobby-Time"));
        PlayFile(PlayDropdownScript.selectedMap);
        Debug.Log("Notespeed: " + MenuScript.noteSpeed);
        using (StreamReader sr = new StreamReader(Application.dataPath + "/MapFolder/" + PlayDropdownScript.selectedMap + ".txt"))
        {
            string line;
            // Read and display lines from the file until the end of
            // the file is reached.
            line = sr.ReadLine();
            distance1 = Array.ConvertAll(line.Split(' '), float.Parse);
            line = sr.ReadLine();
            distance2 = Array.ConvertAll(line.Split(' '), float.Parse);
            line = sr.ReadLine();
            distance3 = Array.ConvertAll(line.Split(' '), float.Parse);
            line = sr.ReadLine();
            distance4 = Array.ConvertAll(line.Split(' '), float.Parse);
        }
        CreateScript.creating = false;
        NoteScript.moving = true;
        songData.volume = MenuScript.songVolume;
        //songData.Play();
        foreach (float x in distance1)
        {
            GameObject newNote = Instantiate(Note, Chord.transform);
            newNote.transform.SetParent(Chord.transform);
            newNote.transform.localPosition = new Vector2((x * MenuScript.noteSpeed), transform.localPosition.y);
        }
        foreach (float x in distance2)
        {
            GameObject newNote1 = Instantiate(Note1, Chord1.transform);
            newNote1.transform.SetParent(Chord1.transform);
            newNote1.transform.localPosition = new Vector2((x * MenuScript.noteSpeed), transform.localPosition.y);
        }
        foreach (float x in distance3)
        {
            GameObject newNote2 = Instantiate(Note2, Chord2.transform);
            newNote2.transform.SetParent(Chord2.transform);
            newNote2.transform.localPosition = new Vector2((x * MenuScript.noteSpeed), transform.localPosition.y);
        }
        foreach (float x in distance4)
        {
            GameObject newNote3 = Instantiate(Note3, Chord3.transform);
            newNote3.transform.SetParent(Chord3.transform);
            newNote3.transform.localPosition = new Vector2((x * MenuScript.noteSpeed), transform.localPosition.y);
        }
        score = 0;
        combo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            MenuScript.menuMusicStarted = false;
            SceneManager.LoadScene(0);
        }

        songDuration = (int)songData.clip.length - (int)songData.time;
        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + combo + "X";
        duration.text = "Duration: " + songDuration;
    }
}
