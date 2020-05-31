using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAnswerButton : MonoBehaviour
{
    [SerializeField] private Button          button     = null;
    [SerializeField] private TextMeshProUGUI txt_answer = null;

    
    public void init( string text, Action callback )
    {
        if (callback == null)
        {
            Debug.LogError( $"callback is null!" );
            return;
        }
        
        button.onClick.AddListener( () => callback.Invoke() );
        
        txt_answer.text = text;
    }
}
