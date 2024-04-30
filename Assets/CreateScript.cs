using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateScript : MonoBehaviour
{
    public GameObject Note;//all of the needed variabled
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
    void Start()//gets rid of the menu music and creates a onclick listener for the create map button
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
    void Update()//used to escape the scene and also to create notes at the specific point in the song that the player wants
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

        if (Input.GetKeyDown(KeyCode.M))//each one of these functions is used to place notes on the screen for the player and to add the notes to a list of child objects of the specified chord
        {
            chordList.Add(song.time);
            GameObject newNote = Instantiate(Note, Chord.transform);
            newNote.transform.parent = Chord.transform;
            newNote.transform.localPosition = new Vector2(0, 0);
            chordNum++;

        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            chord1List.Add(song.time);
            GameObject newNote1 = Instantiate(Note1, Chord1.transform);
            newNote1.transform.parent = Chord1.transform;
            newNote1.transform.localPosition = new Vector2(0, 0);
            chordNum1++;

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            chord2List.Add(song.time);
            GameObject newNote2 = Instantiate(Note2, Chord2.transform);
            newNote2.transform.parent = Chord2.transform;
            newNote2.transform.localPosition = new Vector2(0, 0);
            chordNum2++;

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            chord3List.Add(song.time);
            GameObject newNote3 = Instantiate(Note3, Chord3.transform);
            newNote3.transform.parent = Chord3.transform;
            newNote3.transform.localPosition = new Vector2(0, 0);
            chordNum3++;

        }


    }
    void CreateClick()//when the user presses the create map button the game will take the chord lists and create a map file titled after the given mp3
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
