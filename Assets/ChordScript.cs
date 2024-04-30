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
        if (CreateScript.creating == false)
        {
            ChordLogic();
        }
    }

    public void ChordLogic()
    {
        if (transform.childCount <= 0)
        {
            return;
        }
        Transform child = transform.GetChild(0);
        if (child.localPosition.x < -5f)
        {
            PlayScript.combo = 0;
            Destroy(child.gameObject);
        }
        else
        {
            if (!Input.GetKeyDown(chordKey))
            {
                return;
            }
            if (Mathf.Abs(child.localPosition.x) < pressRange)
            {
                PlayScript.score += 1 * PlayScript.combo;
                if (PlayScript.combo < 5)
                {
                    PlayScript.combo++;
                }
                Destroy(child.gameObject);
            }
            else
            {
                PlayScript.combo = 0;
            }
        }
    }
}
