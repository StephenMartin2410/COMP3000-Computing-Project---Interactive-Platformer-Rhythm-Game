using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    public TMP_Text volumeSliderText;
    public TMP_Text NoteSpeedSliderText;
    //public AudioSource volumeChange;
    public Slider volumeSlider;
    public Slider noteSpeedSlider;

    // Start is called before the first frame update
    void Start()
    {
        //AudioSource volumeChange = GameObject.FindGameObjectWithTag("Lobby-Time").GetComponent<AudioSource>();
        noteSpeedSlider.value = MenuScript.noteSpeed;
        volumeSlider.value = MenuScript.songVolume;
    }

    // Update is called once per frame
    void Update()
    {

        MenuScript.noteSpeed = noteSpeedSlider.value;
        NoteSpeedSliderText.text = Convert.ToString(MenuScript.noteSpeed);
        MenuScript.songVolume = volumeSlider.value;
        AudioListener.volume = volumeSlider.value;
        volumeSliderText.text = Convert.ToString(Convert.ToUInt32(MenuScript.songVolume * 100));
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            MenuScript.menuMusicStarted = false;
            SceneManager.LoadScene(0);
        }
    }
}
