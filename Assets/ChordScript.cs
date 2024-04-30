using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordScript : MonoBehaviour
{

    public string chordKey;

    public float pressRange;
    // Start is called before the first frame update
    public void Start()
    {

    }

    private void Update()
    {
        if (!CreateScript.creating)
        {
            ChordLogic();
        }
    }

    public void ChordLogic()
    {
        if (base.transform.childCount <= 0)
        {
            return;
        }
        Transform child = base.transform.GetChild(0);
        if (child.localPosition.x < -1f)
        {
            PlayScript.combo = 0;
            Object.Destroy(child.gameObject);
        }
        else
        {
            if (!Input.GetKeyDown(chordKey))
            {
                return;
            }
            if (Mathf.Abs(child.localPosition.x) < pressRange)
            {
                PlayScript.score += 5 * PlayScript.combo;
                if (PlayScript.combo < 5)
                {
                    PlayScript.combo++;
                }
                Object.Destroy(child.gameObject);
            }
            else
            {
                PlayScript.combo = 0;
            }
        }
    }
}
