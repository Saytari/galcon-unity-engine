using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    // images components
    Image[] images;

    // text components
    Text[] texts;

    // button component
    protected Button button;

    // image component
    protected Image image;

    // Start is called before the first frame update
    void Start()
    {
        // get button component
        button = GetComponent<Button>();

        // get image component
        image = GetComponent<Image>();

        // get all image components in children
        images = GetComponentsInChildren<Image>();

        // get all text components in children
        texts = GetComponentsInChildren<Text>();

        // add click callback
        button.onClick.AddListener(change);
    }

    protected void fadeOut()
    {
        foreach(Text text in texts)
            text.color = new Color(1f, 1f, 1f, 0.5f);

        foreach(Image image in images)
            image.color = new Color(1f, 1f, 1f, 0.5f);

        image.color = new Color(1f, 1f, 1f, 0.5f);
    }

    protected void fadeIn()
    {
        foreach(Text text in texts)
            text.color = new Color(1f, 1f, 1f, 1f);

        foreach(Image image in images)
            image.color = new Color(1f, 1f, 1f, 1f);

        image.color = new Color(1f, 1f, 1f, 1f);
    }

    protected virtual void change()
    {

    }
}
