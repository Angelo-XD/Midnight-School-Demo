using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Note : MonoBehaviour
{
    public TMP_Text NoteText;
    [TextArea(3,10)]
    public string Text;

    private void Start()
    {
        NoteText.text = Text;
    }
}
