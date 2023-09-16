using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor : MonoBehaviour
{
    public int type; //1 for shop, 2 for shrine
    private string type_str;

    public int[] cam_prices;
    public int[] stealth_prices;

    private int[] active_prices;
    private int idx;

    public Text balance_text;

    private PlayerInfo p;

    // Start is called before the first frame update
    void Start()
    {
        this.p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();

        if (type == 0)
        {
            type_str = "Camera";
            active_prices = cam_prices;
        } else
        {
            type_str = "Stealth";
            active_prices = stealth_prices;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateMessage()
    {
        if (type == 0)
        {
            //If cam lvl = 1, want price of cam_lvl 2
            //cam_lvl 2 is stored in active_prices[0]
            if (p.cam_lvl == 1)
            {
                idx = 1;
            }
            else if (p.cam_lvl == 2)
            {
                idx = 0;
            }
            else {idx = 2;}
        }
        else
        {
            if (p.stealth_lvl == 1)
            {
                idx = 0;
            }
            else if (p.stealth_lvl == 2)
            {
                idx = 1;
            }
            else
            {
                idx = 2;
            }
        }

        if (idx < 2)
        {
            GameObject.FindGameObjectWithTag("Vending").GetComponent<NoticeScreen>().open("Upgrade " + this.type_str + "? \n Next upgrade costs: " + active_prices[idx]);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Vending").GetComponent<NoticeScreen>().open("Your " + this.type_str + "is fully upgraded!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Vending").GetComponent<NoticeScreen>().active_vendor = this;
            updateMessage();
            
        }
    }

    public void tryToBuy()
    {
        if (idx >= 2)
        {
            return;
        }
        if (p.balance >= active_prices[idx])
        {
            p.balance -= active_prices[idx];
            if (type == 0)
            {
                p.cam_lvl++;
            }
            else
            {
                p.stealth_lvl++;
            }
        }
        updateMessage();
    }
}
