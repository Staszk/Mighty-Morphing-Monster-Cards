using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardToken : MonoBehaviour
{
    public TMP_Text numberText;
    public TMP_Text nameText;

    private DeckBuilder parent;

    public void SetUp(string name, int count, DeckBuilder parent)
    {
        nameText.text = name;
        numberText.text = count.ToString();
        this.parent = parent;
    }

    public void SetCount(int count)
    {
        numberText.text = count.ToString();
    }

    public void RemoveCard()
    {
        AudioManagerScript.instance.Play("Death");
        parent.RemoveCardFromList(nameText.text);
    }
}
