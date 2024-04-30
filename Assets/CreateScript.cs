using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateScript : MonoBehaviour
{
    public GameObject Note;
    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;
    public GameObject Chord;
    public GameObject Chord1;
    public GameObject Chord2;
    public GameObject Chord3;
    public AudioSource song;
    public Button createButton;
    public static bool creating;
    public List<float> chordList = new List<float>();
    public List<float> chord1List = new List<float>();
    public List<float> chord2List = new List<float>();
    public List<float> chord3List = new List<float>();
    public int chordNum;
    public int chordNum1;
    public int chordNum2;
    public int chordNum3;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("Lobby-Time"));
        createButton.onClick.AddListener(() => CreateClick());
        creating = true;
        NoteScript.moving = false;
        chordNum = 0;
        chordNum1 = 0;
        chordNum2 = 0;
        chordNum3 = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (SliderScript.songPlaying == true)
        {
            NoteScript.moving = true;

        }
        else
        {
            NoteScript.moving = false;

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            chordList.Add(song.time);
            GameObject newNote = Instantiate(Note, Chord.transform);
            newNote.transform.localPosition = new Vector2(0, 0);
            chordNum++;

        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            chord1List.Add(song.time);
            GameObject newNote = Instantiate(Note1, Chord1.transform);
            newNote.transform.localPosition = new Vector2(0, 0);
            chordNum1++;

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            chord2List.Add(song.time);
            GameObject newNote = Instantiate(Note2, Chord2.transform);
            newNote.transform.localPosition = new Vector2(0, 0);
            chordNum2++;

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            chord3List.Add(song.time);
            GameObject newNote = Instantiate(Note3, Chord3.transform);
            newNote.transform.localPosition = new Vector2(0, 0);
            chordNum3++;

        }


    }
    void CreateClick()
    {
        var c = string.Join(" ", chordList);
        var c1 = string.Join(" ", chord1List);
        var c2 = string.Join(" ", chord2List);
        var c3 = string.Join(" ", chord3List);
        if (File.Exists(Application.dataPath + "/MapFolder" + "/ " + CreateDropdownScript.selectedMp3 + ".txt"))
        {
            File.Delete(Application.dataPath + "/MapFolder" + "/ " + CreateDropdownScript.selectedMp3 + ".txt");
        }
        using (StreamWriter sw = new StreamWriter(Application.dataPath + "/MapFolder" + "/ " + CreateDropdownScript.selectedMp3 + ".txt", true))
        {
            sw.WriteLine(c);
            sw.WriteLine(c1);
            sw.WriteLine(c2);
            sw.WriteLine(c3);
        }
    }
}
