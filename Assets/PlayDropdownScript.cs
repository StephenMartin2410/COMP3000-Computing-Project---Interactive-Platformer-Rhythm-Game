using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayDropdownScript : MonoBehaviour
{
    public static string selectedMap;
    List<string> options = new List<string>();
    TMP_Dropdown playDropdown;




    // Start is called before the first frame update
    void Start()
    {
        playDropdown = GetComponent<TMP_Dropdown>();
        playDropdown.ClearOptions();
        foreach (string fileName in Directory.GetFiles(Application.dataPath + "/MapFolder/", "*.txt"))
        {
            var tempPathName = Path.GetFileName(fileName);
            char[] trim = { '.', 't', 'x', 't' };
            string pathName = tempPathName.TrimEnd(trim);
            options.Add(pathName);
        }

        playDropdown.AddOptions(options);
    }

    // Update is called once per frame
    void Update()
    {
        selectedMap = playDropdown.options[playDropdown.value].text;
    }
}
