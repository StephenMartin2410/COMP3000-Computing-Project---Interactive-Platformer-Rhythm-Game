using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateDropdownScript : MonoBehaviour
{
    public static string selectedMp3;
    List<string> options = new List<string>();
    TMP_Dropdown createDropdown;




    // Start is called before the first frame update
    void Start()
    {
        createDropdown = GetComponent<TMP_Dropdown>();
        createDropdown.ClearOptions();
        foreach (string fileName in Directory.GetFiles(Application.dataPath + "/MP3Folder/", "*.mp3"))
        {
            var tempPathName = Path.GetFileName(fileName);
            char[] trim = { '.', 'm', 'p', '3' };
            string pathName = tempPathName.TrimEnd(trim);
            options.Add(pathName);
        }

        createDropdown.AddOptions(options);
    }

    // Update is called once per frame
    void Update()
    {
        selectedMp3 = createDropdown.options[createDropdown.value].text;
    }
}
