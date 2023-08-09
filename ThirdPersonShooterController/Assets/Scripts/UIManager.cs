using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text HintInfo;
    public Button collapseExpandBtn;
    public Image buttonIcon;

    public Sprite collapseImg;
    public Sprite expandImg;

    bool collapsed = false;

    public void btnOnClick()
    {
        if (collapsed)
        {
            HintInfo.gameObject.SetActive(false);
            buttonIcon.sprite = expandImg;
        }
        else
        {
            HintInfo.gameObject.SetActive(true);
            buttonIcon.sprite = collapseImg;
        }
        collapsed = !collapsed;
    }
}
