using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public string cover_type;

    public GameObject[] birdsContained;

    private float prob_sum;
    private float[] prob_ranges;
    
    // Start is called before the first frame update
    void Start()
    {
        this.prob_sum = 0;
        List<float> ranges = new List<float>();
        ranges.Add(0.0f);
        print("This is for " + cover_type);
        for (int i = 0; i < birdsContained.Length; i++)
        {
            var g = birdsContained[i];
            this.prob_sum += g.GetComponent<Bird>().appearance_prob;
            ranges.Add(this.prob_sum);
            print(g.GetComponent<Bird>().bird_type + " at " + this.prob_sum);
        }

        this.prob_ranges = ranges.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Bird checkBird()
    {
        float roll = Random.Range(0.0f, this.prob_sum);

        for (int i = 1; i < prob_ranges.Length; i++)
        {
            if (roll >= prob_ranges[i - 1] && roll <= prob_ranges[i])
            {
                return birdsContained[i - 1].GetComponent<Bird>();
            }
        }
        return null;
    }

    void captureSuccess(Bird b)
    {
        PlayerInfo p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();

        if (b.cam_req > p.cam_lvl) 
        {
            p.balance = (int)((float)(p.balance) * 0.90f);
            var go = GameObject.FindGameObjectWithTag("Notice");
            go.GetComponent<NoticeScreen>().open("You were attacked by a " + b.bird_type);
        }
        else if (b.stealth_req > p.stealth_lvl)
        {
            var go = GameObject.FindGameObjectWithTag("Notice");
            go.GetComponent<NoticeScreen>().open("You spotted a " + b.bird_type + " but it flew away!");
        }
        else 
        {
            p.balance += b.price;
            var go = GameObject.FindGameObjectWithTag("Notice");
            go.GetComponent<NoticeScreen>().open("You took a picture of " + b.bird_type);

        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Bird birdFound = checkBird();

            if (birdFound != null)
            {
                captureSuccess(birdFound);
            }
        }
        
    }
}
