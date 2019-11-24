using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerOptionButton : MonoBehaviour
{
    public delegate void OptionSelected(DialogAnswerOption option);
    public OptionSelected onOptionSelected = (i) => { };

    public Text label;
    public Button button;

    private DialogAnswerOption option;
    private int number;

    public void SetData(DialogAnswerOption option)
    {
        this.option = option;

        number = transform.GetSiblingIndex() + 1;
        if (number >= 0)
        {
            label.text = "[" + number + "] " + option.text;
        }
        else
        {
            label.text = option.text;
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1) && number == 1) ||
            (Input.GetKeyDown(KeyCode.Alpha2) && number == 2) ||
            (Input.GetKeyDown(KeyCode.Alpha3) && number == 3) ||
            (Input.GetKeyDown(KeyCode.Alpha4) && number == 4) ||
            (Input.GetKeyDown(KeyCode.Alpha5) && number == 5))
        {
            onOptionSelected(option);
        }
    }

    public void OnButtonClick()
    {
        onOptionSelected(option);
    }
}
