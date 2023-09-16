using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetter : MonoBehaviour
{
    private PlayerInfo info;
    [SerializeField]
    private int idx;
    // Start is called before the first frame update
    void Start()
    {
        this.info = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (idx == 1)
        {
            gameObject.GetComponent<Text>().text = "Level: " + this.info.cam_lvl;
        } 
        else if (idx == 2)
        {
            gameObject.GetComponent<Text>().text = "Level: " + this.info.stealth_lvl;
        }
        else if (idx == 3)
        {
            gameObject.GetComponent<Text>().text = "Current Balance: $" + this.info.balance + ".00";
        }

    }
}
