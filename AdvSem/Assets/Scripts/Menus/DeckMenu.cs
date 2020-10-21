using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckMenu : MonoBehaviour
{
    public Animator anim;

    public void TransitionAnimation(int animation)
    {
        anim.SetInteger("transition", animation);
    }
}
