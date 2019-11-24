using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DialogView : MonoBehaviour
{
    public delegate void NextLine();
    public delegate void AnswerSelected(DialogAnswerOption answer);
    public NextLine OnNextLine = () => { };
    public AnswerSelected OnAnswer = (answer) => { };

    public GameObject view;
    public Text textSaid;
    public float speed;

    public Transform buttonsPanel;
    public ObjectPool buttonsPool;

    private bool isOn;

    private string currentLine;
    private int currentChar;

    private void Start()
    {
        Hide();
    }

    public void Show(string line)
    {
        isOn = true;
        view.SetActive(true);

        currentChar = 0;
        currentLine = line;
        StartCoroutine(Speak());
    }

    private IEnumerator Speak()
    {
        while (currentLine.Length > currentChar++)
        {
            textSaid.text = currentLine.Substring(0, currentChar);
            yield return new WaitForSeconds(speed);
        }
    }

    private void Update()
    {
        if (isOn && Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            OnNextLine();
        }
    }

    public void ShowAnswerOptions(DialogAnswerOption[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            buttonsPanel.gameObject.SetActive(true);
            AnswerOptionButton answerOption = buttonsPool.GetObject().GetComponent<AnswerOptionButton>();
            answerOption.transform.SetParent(buttonsPanel);
            answerOption.transform.SetSiblingIndex(i);
            answerOption.SetData(options[i]);
            answerOption.onOptionSelected += AnswerOptionButton_OnOptionSelected;
        }
    }

    private void AnswerOptionButton_OnOptionSelected(DialogAnswerOption option)
    {
        RemoveAllAnswers();
        OnAnswer(option);
    }

    private void RemoveAllAnswers()
    {
        for (int i = buttonsPanel.childCount - 1; i >= 0; i--)
        {
            AnswerOptionButton answerOption = buttonsPanel.GetChild(i).GetComponent<AnswerOptionButton>();
            answerOption.onOptionSelected -= AnswerOptionButton_OnOptionSelected;
            buttonsPool.ReturnToPool(answerOption.gameObject);
        }
        buttonsPanel.gameObject.SetActive(false);
    }

    public void Hide()
    {
        isOn = false;
        view.SetActive(false);
        buttonsPanel.gameObject.SetActive(false);
    }
}
