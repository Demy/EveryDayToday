using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    public delegate void NextLine();
    public NextLine OnNextLine = () => { };

    public GameObject view;
    public Text textSaid;

    private bool isOn;

    private void Start()
    {
        Hide();
    }

    public void Show(string line)
    {
        isOn = true;
        view.SetActive(true);
    }

    public void Hide()
    {
        isOn = false;
        view.SetActive(false);
    }

    private void Update()
    {
        if (isOn && Input.GetKeyDown(KeyCode.Space))
        {
            OnNextLine();
        }
    }
}
