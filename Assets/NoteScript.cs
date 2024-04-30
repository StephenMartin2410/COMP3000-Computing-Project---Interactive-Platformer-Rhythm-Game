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
        if (moving)
        {
            base.transform.position -= Vector3.right * MenuScript.noteSpeed * Time.deltaTime;
        }
    }
}
