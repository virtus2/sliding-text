using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect), typeof(Mask), typeof(Image))]
public class SlidingText : MonoBehaviour
{
    enum ScrollMoveDirection
    {
        ToFront,
        ToEnd,
    }

    private ScrollRect scrollRect;
    private Mask mask;
    private Image maskImage;
    private Text text;
    private RectTransform rectTransform; // RectTransform cache

    [Header("If true, Text will go back to front immediately")]
    public bool scrollToFrontImmediately;

    [Header("How much will text go back to front?"), Range(0.001f, 1f)]
    public float scrollToFrontSpeed = 0.005f;

    [Header("How much will text move to end each update"), Range(0.001f, 1f)]
    public float scrollToEndSpeed = 0.005f;

    [Header("How long to pause at the end of the text?")]
    public float endStopTime = 0.5f;
    private float currentEndStopTime = 0.0f;

    [Header("How long to pause at the front of the text?")]
    public float frontStopTime = 0.5f;
    private float currentFrontStopTime = 0.0f;


    private ScrollMoveDirection direction = ScrollMoveDirection.ToEnd;
    private Color maskImageColor = new Color(1f, 1f, 1f, 0.01f);
    private float position = 0.0f;

    private void Awake()
    {
        if (!scrollRect)
        {
            scrollRect = GetComponent<ScrollRect>();
        }
        if (!mask)
        {
            mask = GetComponent<Mask>();
        }
        if (!maskImage)
        {
            maskImage = GetComponent<Image>();
        }
        if(!rectTransform)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        maskImage.raycastTarget = false;
        mask.enabled = true;
    }

    private void OnValidate()
    {
        if (!scrollRect)
        {
            scrollRect = GetComponent<ScrollRect>();
        }
        if (!mask)
        {
            mask = GetComponent<Mask>();
        }
        if (!maskImage)
        {
            maskImage = GetComponent<Image>();
        }
        if (!rectTransform)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        if (scrollRect && !(scrollRect.content))
        {
            GameObject go = new GameObject("Text");
            text = go.AddComponent<Text>();
            text.text = "The Quick Brown Fox Jumps Over The Lazy Dog";
            text.alignment = TextAnchor.MiddleCenter;
            text.raycastTarget = false; // Set this true if need to raycast text.
            
            RectTransform textRectTransform = text.rectTransform;
            textRectTransform.parent = transform;
            textRectTransform.sizeDelta = new Vector2(text.preferredWidth, rectTransform.rect.height);
            textRectTransform.localPosition = Vector2.zero;

            scrollRect.content = textRectTransform;
        }
        
        if(mask)
        {
            // Turn off the mask so that you can see the text in the editor.
            mask.showMaskGraphic = false;
        }
        if(maskImage)
        {
            maskImage.color = maskImageColor;
        }
    }

    private void LateUpdate()
    {
        if(Mathf.Approximately(position, 1.0f))
        {
            // Reset the stop timer before text goes front
            currentFrontStopTime = 0f;
            direction = ScrollMoveDirection.ToFront;
        }
        else if(Mathf.Approximately(position, 0.0f))
        {
            // Reset the stop timer before text goes end
            currentEndStopTime = 0f;
            direction = ScrollMoveDirection.ToEnd;
        }

        switch(direction)
        {
            case ScrollMoveDirection.ToFront:
                ScrollTextToFront();
                break;
            case ScrollMoveDirection.ToEnd:
                ScrollTextToEnd();
                break;
        }

        scrollRect.horizontalNormalizedPosition = position;
    }

    private void ScrollTextToFront()
    {
        if (scrollToFrontImmediately)
        {
            position = 0;
            return;
        }

        if (currentEndStopTime < endStopTime)
        {
            currentEndStopTime += Time.deltaTime;
        }
        else
        {
            position = Mathf.Clamp(position - scrollToFrontSpeed, 0f, 1f);
        }
    }

    private void ScrollTextToEnd()
    {
        if (currentFrontStopTime < frontStopTime)
        {
            currentFrontStopTime += Time.deltaTime;
        }
        else
        {
            position = Mathf.Clamp(position + scrollToEndSpeed, 0f, 1f);
        }
    }
}
