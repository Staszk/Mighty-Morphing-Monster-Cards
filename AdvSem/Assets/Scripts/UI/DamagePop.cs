using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePop : MonoBehaviour
{
    public void KillSelf()
    {
        Destroy(gameObject);
    }

    public void Set(int damage)
    {
        transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = damage.ToString();
        transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = damage.ToString();
    }
}
