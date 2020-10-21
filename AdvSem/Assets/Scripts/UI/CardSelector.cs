using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class CardSelector : MonoBehaviour
{
    public static event Action<CardSelector, CardInfo> ActionSelectorPressed = delegate { };

    public TMP_Text typeText;
    public TMP_Text nameText;

    public CardInfo cardInfo;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        nameText.text = cardInfo.name;

        if (cardInfo.GetType() == typeof(MonsterCardInfo))
        {
            typeText.text = "M";
        }
        else if (cardInfo.GetType() == typeof(ElementCardInfo))
        {
            typeText.text = "E";
        }
        else
        {
            typeText.text = "A";
        }
    }

    public void EnableSelector(bool enabled)
    {
       button.interactable = enabled;
    }

    public void SelectCard()
    {
        ActionSelectorPressed(this, cardInfo);
    }

    public bool IsEnabled()
    {
        return button.interactable;
    }
}
