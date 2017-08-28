using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour {

    public Text MessageText;
    public Text StepIndicator;
    public Text MemoryIndicator;

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
        ClearText();
    }
    #endregion
    
    private void ClearText()
    {
        MessageText.text = string.Empty;
        MemoryIndicator.text = string.Empty;
        StepIndicator.text = string.Empty;
    }

    public static void SetTextFromQueue(List<CommandQueue.Commands> commandQueue)
    {
        string queueString = string.Empty;
        for (int i = 0; i < commandQueue.Count; i++)
        {
            queueString += "\n>> " + ObjectSelector.GetSelectedObject().name + "." + commandQueue[i].ToString() + "();";
        }
        Instance.MessageText.text = queueString;
    }

    public static void ClearStepIndicator()
    {
        Instance.StepIndicator.text = string.Empty;
    }

    public static void SetStepIndicator(int index)
    {
        string indicatorText = string.Empty;
        for (int i = 0; i <= index; i++)
        {
            indicatorText += "\n";
        }
        indicatorText += ">";

        Instance.StepIndicator.text = indicatorText;
    }

    public static void SetMemoryInidcator(int availableMem, int maxMem)
    {
        string memoryText = "Available Memory: " + availableMem + "/" + maxMem;
        Instance.MemoryIndicator.text = memoryText;
    }
}
