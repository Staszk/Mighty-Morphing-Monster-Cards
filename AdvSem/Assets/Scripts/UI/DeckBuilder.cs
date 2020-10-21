using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CardSelection
{
    public CardSelection(CardInfo card, CardToken token)
    {
        this.token = token;
        this.card = card;
        cardCount = 1;
    }

    public CardToken token;
    public CardInfo card;
    public int cardCount;
}

public class DeckBuilder : MonoBehaviour
{
    public List<CardSelection> selectedCards;
    public CardSelector[] selectors;

    public Button saveDeckButton;
    public TMP_Text countText;

    public GameObject cardTokenPrefab;

    private int cardCount;

    private Vector2 sizeDelta;

    private void OnEnable()
    {
        sizeDelta = GetComponent<RectTransform>().sizeDelta;

        CardSelector.ActionSelectorPressed += AddCardToList;
    }

    private void OnDisable()
    {
        CardSelector.ActionSelectorPressed -= AddCardToList;
    }

    private void AddCardToList(CardSelector selector, CardInfo card)
    {
        bool exists = false;

        for (int i = 0; i < selectedCards.Count; i++)
        {
            if (selectedCards[i].card.name == card.name)
            {
                selectedCards[i].cardCount++;
                selectedCards[i].token.SetCount(selectedCards[i].cardCount);

                if (selectedCards[i].cardCount == 3)
                {
                    selector.EnableSelector(false);
                }

                exists = true;
                break;
            }
        }

        if (!exists)
        {
            CardToken t = Instantiate(cardTokenPrefab, transform).GetComponent<CardToken>();
            t.SetUp(card.name, 1, this);
            selectedCards.Add(new CardSelection(card, t));

            if (transform.childCount > 8)
            {
                sizeDelta.y += 85;

                GetComponent<RectTransform>().sizeDelta = sizeDelta;
            }
        }

        cardCount++;
        UpdateCountText();

        AudioManagerScript.instance.Play("Deck Add");

        if (cardCount == 30)
        {
            foreach (CardSelector s in selectors)
            {
                s.EnableSelector(false);
            }

            saveDeckButton.interactable = true;
        }
    }

    public void RemoveCardFromList(string name)
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            if (selectedCards[i].card.name == name)
            {
                CardSelection card = selectedCards[i];
                card.cardCount--;

                if (card.cardCount == 0)
                {
                    selectedCards.Remove(card);
                    Destroy(card.token.gameObject);
                }
                else
                {
                    card.token.SetCount(card.cardCount);
                }

                EvaluateSelectors();
                cardCount--;
                UpdateCountText();

                if (cardCount < 30)
                {
                    saveDeckButton.interactable = false;
                }

                return;
            }
        }
    }

    private void UpdateCountText()
    {
        countText.text = cardCount.ToString() + " / 30";
    }

    private void EvaluateSelectors()
    {
        for (int i = 0; i < selectors.Length; i++)
        {
            bool inPool = false;
            string cardName = selectors[i].nameText.text;

            foreach (CardSelection cs in selectedCards)
            {
                if (cs.card.name == cardName)
                {
                    inPool = true;
                    if (cs.cardCount < 3)
                    {
                        selectors[i].EnableSelector(true);
                    }
                }
            }

            if (!inPool)
            {
                selectors[i].EnableSelector(true);
            }
        }
    }

    public void SaveDeck()
    {
        List<CardInfo> deck = new List<CardInfo>();

        foreach (CardSelection card in selectedCards)
        {
            for (int i = 0; i < card.cardCount; i++)
            {
                deck.Add(card.card);
            }
        }

        PersistentDataManager.SetDeck(deck);
    }
}
