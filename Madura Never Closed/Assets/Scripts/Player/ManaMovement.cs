using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaMovement : MonoBehaviour
{
    public Image manaBar;
    public int manaAmount = 1000;

    private void Update()
    {
        //Debug.Log(manaAmount);
    }
    public void manaPlayerMove(int mana)
    {
        manaAmount -= mana;
        manaBar.fillAmount = manaAmount / 1000f;
    }

    public void playerSleep(int manaSleep)
    {
        manaAmount += manaSleep;
        manaAmount = Mathf.Clamp(manaAmount, 0, 1000);
        manaBar.fillAmount = manaAmount / 1000f;
    }
}
