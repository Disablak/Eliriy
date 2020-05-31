using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;


public class DialogStory : MonoBehaviour
{
    [SerializeField] private TextAsset       test_text_asset = null;
    [SerializeField] private TextMeshProUGUI txt_main        = null;

    [SerializeField] private Transform      root_answers     = null;
    [SerializeField] private UIAnswerButton ui_answer_button = null;


    private Story test_story = null;

    private void Awake()
    {
        test_story = new Story(test_text_asset.text);
        loadStoryText();
    }

    private void loadStoryText()
    {
        if (test_story.canContinue)
        {
            txt_main.text = test_story.ContinueMaximally();
        }

        spawnAnswers();
    }

    private void spawnAnswers()
    {
        List<Choice>         choices        = test_story.currentChoices;
        List<UIAnswerButton> answer_buttons = new List<UIAnswerButton>();

        if ( choices.Count == 0 )
            closeDialog();
        
        for (var i = 0; i < choices.Count; i++)
        {
            Choice choice = choices[i];
            int    index  = i;

            UIAnswerButton answer_button = Instantiate(ui_answer_button, root_answers);
            answer_button.init(choice.text, makeAnswer);

            answer_buttons.Add(answer_button);

            void makeAnswer()
            {
                test_story.ChooseChoiceIndex(index);
                loadStoryText();

                //Destroy buttons
                foreach (var btn in answer_buttons)
                    Destroy(btn.gameObject);
            }
        }
    }

    private void closeDialog()
    {
        gameObject.SetActive(false);
    }
}
