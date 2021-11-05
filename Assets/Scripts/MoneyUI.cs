using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text moneyDisplay;

    // Update is called once per frame
    void Update()
    {
        moneyDisplay.text = "$"+PlayerStats.Money;
    }
}
