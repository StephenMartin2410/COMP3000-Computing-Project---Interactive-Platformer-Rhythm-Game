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
                songData.Play();
            }
        }
    }

    void Start()
    {
        Debug.Log(PlayDropdownScript.selectedMap);
        PlayFile(PlayDropdownScript.selectedMap);
        UnityEngine.Object.Destroy(GameObject.Find("Lobby-Time"));
        using (StreamReader streamReader = new StreamReader(Application.dataPath + "/MapFolder/" + PlayDropdownScript.selectedMap + ".txt"))
        {
            string text = streamReader.ReadLine();
            distance1 = Array.ConvertAll(text.Split(' '), float.Parse);
            text = streamReader.ReadLine();
            distance2 = Array.ConvertAll(text.Split(' '), float.Parse);
            text = streamReader.ReadLine();
            distance3 = Array.ConvertAll(text.Split(' '), float.Parse);
            text = streamReader.ReadLine();
            distance4 = Array.ConvertAll(text.Split(' '), float.Parse);
        }
        CreateScript.creating = false;
        NoteScript.moving = true;
        songData.volume = MenuScript.songVolume;
        songDuration = songData.clip.length;
        float[] array = distance1;
        foreach (float num in array)
        {
            GameObject obj = UnityEngine.Object.Instantiate(Note);
            obj.transform.parent = Chord.transform;
            obj.transform.localPosition = new Vector2(num * MenuScript.noteSpeed + 1f, base.transform.position.y);
        }
        array = distance2;
        foreach (float num2 in array)
        {
            GameObject obj2 = UnityEngine.Object.Instantiate(Note1);
            obj2.transform.parent = Chord1.transform;
            obj2.transform.localPosition = new Vector2(num2 * MenuScript.noteSpeed + 1f, base.transform.position.y);
        }
        array = distance3;
        foreach (float num3 in array)
        {
            GameObject obj3 = UnityEngine.Object.Instantiate(Note2);
            obj3.transform.parent = Chord2.transform;
            obj3.transform.localPosition = new Vector2(num3 * MenuScript.noteSpeed + 1f, base.transform.position.y);
        }
        array = distance4;
        foreach (float num4 in array)
        {
            GameObject obj4 = UnityEngine.Object.Instantiate(Note3);
            obj4.transform.parent = Chord3.transform;
            obj4.transform.localPosition = new Vector2(num4 * MenuScript.noteSpeed + 1f, base.transform.position.y);
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
