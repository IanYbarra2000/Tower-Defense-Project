using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update

    public static int Money;
    /*[SerializeField]private int _money=100;
    public int money {
        get{ return _money;}
        set{ 
            if(_money == value){
                return;
            }
            _money = value;
            moneyDisplay.text = "$"+_money;
        }
    }*/
    //public static int Money;
    public int startMoney=100;
    void Start()
    {
        Money = startMoney;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
