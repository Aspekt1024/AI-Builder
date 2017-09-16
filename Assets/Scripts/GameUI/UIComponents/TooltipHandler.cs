using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipHandler : MonoBehaviour {

    public RectTransform TooltipPanel;
    public Text TooltipText;

    public void SetTextFromObject(SelectableObject obj)
    {
        if (!TooltipPanel.gameObject.activeSelf)
        {
            TooltipPanel.gameObject.SetActive(true);
        }
        TooltipPanel.position = GameController.Instance.MainCamera.WorldToScreenPoint(obj.transform.position) + Vector3.up * 30;
        TooltipText.text = obj.name;
    }
    
    public void RemoveTooltip()
    {
        if (TooltipPanel.gameObject.activeSelf)
        {
            TooltipPanel.gameObject.SetActive(false);
        }
    }

}
