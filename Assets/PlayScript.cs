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
    public TMP_Text scoreText;//list of variables to be used
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

    public void PlayFile(string fileName) //this function is used to prep the filepath of the selected song that needs to be played
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

        StartCoroutine(LoadAndPlay(uri));//links to the function below
    }


    IEnumerator LoadAndPlay(Uri uri) //this ienumerator function is used to get a local audio file using the unitywebrequest functions
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);//displays the error code if the file cannot be found
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                songData.clip = clip;
                songDuration = songData.clip.length;
                songData.Play();//plays the audio that has been received once it has loaded
            }
        }
    }

    void Start()//this start function is used to place all of the notes onto to screen so that they will line up and match the notes that were created by the user
    {
        //Debug.Log(PlayDropdownScript.selectedMap);
        //songData.Play();
        SliderScript.songPlaying = false;
        Destroy(GameObject.Find("Lobby-Time"));//gets rid of the menumusic that would play so that it doesnt interuppt the song music
        PlayFile(PlayDropdownScript.selectedMap);//runs the coroutine functions above start
        Debug.Log("Notespeed: " + MenuScript.noteSpeed);
        using (StreamReader sr = new StreamReader(Application.dataPath + "/MapFolder/" + PlayDropdownScript.selectedMap + ".txt"))//reads the map text file of the selected song
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


        CreateScript.creating = false;//this block is used to prep the notes to be able to move along the screen
        NoteScript.moving = true;
        songData.volume = MenuScript.songVolume;
        //songData.Play();



        foreach (float x in distance1)//these foreach loops are used to go through the arrays taken from each line of the map text file and place them in a position in the game worldview so that they will match up with the created map
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
        }//there are 4 separate loops for the 4 different chords and 4 different locations the notes can be

        score = 0;
        combo = 0;
    }

    // Update is called once per frame
    void Update()//used to exit the play scene and to update the songtime, score and combo data
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
