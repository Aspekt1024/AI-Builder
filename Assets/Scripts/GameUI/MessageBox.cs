using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour {

    // Set the following in the inspector
    public Text MessageText;
    public Text StepIndicator;
    public Text MemoryIndicator;

    private Queue messages;
    private ObjectSelector objectSelector;
    
    private void Awake()
    {
        objectSelector = FindObjectOfType<ObjectSelector>();
        ClearText();
    }
    
    private void ClearText()
    {
        MessageText.text = string.Empty;
        MemoryIndicator.text = string.Empty;
        StepIndicator.text = string.Empty;
    }

    public void SetTextFromQueue(List<CommandQueue.Commands> commandQueue)
    {
        string queueString = string.Empty;
        for (int i = 0; i < commandQueue.Count; i++)
        {
            queueString += "\n>> " + objectSelector.GetSelectedObject().name + "." + commandQueue[i].ToString() + "();";
        }
        MessageText.text = queueString;
    }

    public void ClearStepIndicator()
    {
        StepIndicator.text = string.Empty;
    }

    public void SetStepIndicator(int index)
    {
        string indicatorText = string.Empty;
        for (int i = 0; i <= index; i++)
        {
            indicatorText += "\n";
        }
        indicatorText += ">";

        StepIndicator.text = indicatorText;
    }

    public void SetMemoryInidcator(int availableMem, int maxMem)
    {
        string memoryText = "Available Memory: " + availableMem + "/" + maxMem;
        MemoryIndicator.text = memoryText;
    }
}
