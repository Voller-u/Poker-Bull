using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("ÅÆµÄÐòºÅ")]
    public int cardNum;
    // Start is called before the first frame update
    void Start()
    {
        cardNum = GameManager.instance.CardImageToCardNum(GetComponent<SpriteRenderer>().sprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImage(int index)
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.instance.cardsImage[index];
    }
}
