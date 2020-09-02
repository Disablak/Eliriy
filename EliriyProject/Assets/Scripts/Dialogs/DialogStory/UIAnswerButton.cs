using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAnswerButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_answer = null;
    [SerializeField] private Button          button     = null;
    [SerializeField] private Image           image      = null;

    public RectTransform rect_transform => (RectTransform) transform;

    public void init( string text, Action callback )
    {
        if ( callback == null )
        {
            Debug.LogError( $"callback is null!" );
            return;
        }

        txt_answer.text = text;
        button.onClick.AddListener( () => callback.Invoke() );
    }

    public void markButton()
    {
        image.color = Color.green;
    }

    public void disableButton()
    {
        button.interactable = false;
    }
}
