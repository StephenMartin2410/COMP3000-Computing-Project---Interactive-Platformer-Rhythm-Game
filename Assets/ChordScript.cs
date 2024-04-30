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

    private void Update()//used to check if the user is in the create scene or the playing scene
    {
        if (CreateScript.creating == false)
        {
            ChordLogic();
        }
    }

    public void ChordLogic()//if the user is in the playing scene then the notes will move and be deleted as a miss if they go past a certain point
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
            if (Mathf.Abs(child.localPosition.x) < pressRange)//the user will also be able to hit the notes and earn a score and a multiplier if they hit a combo of notes
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
