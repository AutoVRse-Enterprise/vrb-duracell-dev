using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeroInteractionTaskDisplayer : MonoBehaviour
{
    [SerializeField]
    private string[] _tasks;
    [SerializeField]
    private TMP_Text _textDisplayer;
    private int _index = -1;
    public void ShowNextTask(){
        _index++;
        if (_index >= _tasks.Length){
            Debug.LogError ("INDEX OUT OF BOUNDS, UNABLE TO SHOW NEXT TASK. RETURNING.");
            return;
        }
        _textDisplayer.text = _tasks[_index];
    }

}
