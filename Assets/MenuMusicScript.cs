using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicScript : MonoBehaviour
{
    public static MenuMusicScript lobbySong;
    // Start is called before the first frame update
    void Start()//this function is used to check if an instance of the menumusic exists and if it does then it deletes the current running one so that it wont overlap with the dontdestroyonload music that is already playing
    {
        if (lobbySong != null && lobbySong != this)
        {
            Object.Destroy(base.gameObject);
            return;
        }
        lobbySong = this;
        Object.DontDestroyOnLoad(base.gameObject);//makes it so the menumusic runs when in the menu and the options scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
