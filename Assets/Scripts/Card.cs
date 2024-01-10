using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("ÅÆµÄÐòºÅ")]
    public int cardNum;
    public int spriteNum;
    // Start is called before the first frame update
    void Start()
    {
        
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
