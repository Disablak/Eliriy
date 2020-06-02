using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAnswerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_answer = null;
    [SerializeField] private Button          button     = null;

    
    public void init( string text, Action callback )
    {
        if (callback == null)
        {
            Debug.LogError( $"callback is null!" );
            return;
        }
        
        txt_answer.text = text;
        button.onClick.AddListener( () => callback.Invoke() );
    }
}
