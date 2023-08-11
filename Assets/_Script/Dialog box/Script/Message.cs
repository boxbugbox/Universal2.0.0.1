using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public Image headIcon;
    public Text self_adaptation,content;
    float chatItemTextMaxWidth = 311f;
    private void SetHeight()
    {
        RectTransform rect = self_adaptation.GetComponent<RectTransform>();
        float curTextWidth = self_adaptation.preferredWidth;
        if (curTextWidth >= chatItemTextMaxWidth)
        {
            rect.sizeDelta = new Vector2(311f, rect.rect.height);
            self_adaptation.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            self_adaptation.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(404.7f, self_adaptation.preferredHeight + 52f);
        }
    }
    public void UpdateMessage(string mes)
    {   
        self_adaptation.text = content.text = mes;
        SetHeight();
    }
    public void SetHeadIcon(Sprite headImage)
    {
        headIcon.overrideSprite = headImage;
    }
}
