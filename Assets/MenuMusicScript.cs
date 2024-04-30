using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicScript : MonoBehaviour
{
    public static MenuMusicScript lobbySong;
    // Start is called before the first frame update
    void Start()
    {
        if (lobbySong != null && lobbySong != this)
        {
            Object.Destroy(base.gameObject);
            return;
        }
        lobbySong = this;
        Object.DontDestroyOnLoad(base.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
