using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public RectTransform itemEntry;
    public RectTransform content;
    public Mask mask;

    public bool isUpScroller;

    public float yMax;
    public float currentY;
    public float itemEntryHeight;
    public float maskHeight;
    public float contentHeight;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Scroll);
    }

    private void Update()
    {
        if (isUpScroller)
            button.interactable = content.anchoredPosition.y > 0;
        else
            button.interactable = content.anchoredPosition.y < yMax;

        currentY = content.anchoredPosition.y;
        itemEntryHeight = itemEntry.rect.height;
        maskHeight = mask.rectTransform.rect.height;
        contentHeight = content.rect.height;

        yMax = content.rect.height - mask.rectTransform.rect.height;
    }

    public void Scroll()
    {
        var pos = content.anchoredPosition;
        int sign = isUpScroller ? -1 : 1;
        pos.y += sign * itemEntry.rect.height;
        content.anchoredPosition = pos;
    }
}
