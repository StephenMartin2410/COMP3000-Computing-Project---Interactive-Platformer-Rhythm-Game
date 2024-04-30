using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public static bool moving;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (moving == true)
        {
            transform.localPosition -= (Vector3.right * MenuScript.noteSpeed) * Time.deltaTime;//makes the note move to the left of the world view when the user is in the play scene
        }
    }
}
