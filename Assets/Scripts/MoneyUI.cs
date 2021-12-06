using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text moneyDisplay;

    // Don't really like how this is implemented, maybe it would be better with a different name like UI updater and we can put all UI that needs to be updated here
    void Update()
    {
        moneyDisplay.text = "$"+PlayerStats.Money;
    }
}
