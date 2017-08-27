using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour {

    public Text MessageText;

    private Queue messages;

    #region initialisation
    private static MessageBox messageBox;
    public static MessageBox Instance
    {
        get
        {
            if (messageBox == null)
            {
                Debug.LogError("Please place a MessageBox in the scene!");
            }
            return messageBox;
        }
    }

    private void Awake()
    {
        if (messageBox == null)
        {
            messageBox = this;
        }
        else
        {
            Debug.LogError("Detected more than one MessageBox in the scene!");
        }
        AddText();
    }
    #endregion

    public void AddText()
    {
        MessageText.text = MessageText.text + "\ntest";
    }

}
