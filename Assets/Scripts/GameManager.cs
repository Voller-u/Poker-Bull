using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("扑克牌牌面")]
    public List<Sprite> cardsImage = new();

    private void Awake()
    {

    }

    void Start()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
        Utils.Shuffle<Sprite>(cardsImage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Eval(List<Card> cards)
    {
        //炸弹
        int[] numCnt = new int[14];
        foreach (Card card in cards)
        {
            numCnt[card.cardNum]++;
        }
        for (int i = 1; i <= 13; i++)
            if (numCnt[i] >= 4) return 15;

        //五小
        int numSum = 0;
        foreach (Card card in cards)
            numSum += card.cardNum;
        if (numSum <= 10) return 14;

        //金牛
        bool goldBull = true;
        foreach (Card card in cards)
            if (card.cardNum <= 10) goldBull = false;
        if (goldBull)
            return 13;

        //银牛
        bool silverBull = true;
        foreach (Card card in cards)
            if (card.cardNum < 10) silverBull = false;
        if (silverBull)
            return 12;

        if (cards[0].cardNum + cards[1].cardNum + cards[2].cardNum % 10 == 0)
        {
            int score = (cards[3].cardNum % 10 + cards[4].cardNum % 10) % 10;
            return score;
        }

        return 0;
    }

    public int CardImageToCardNum(Sprite cardImage)
    {
        string card = cardImage.name;
        Match match = Regex.Match(card, @"\d+$");
        if (match.Success)
        {
            int cardNum = int.Parse(match.Value);
            return cardNum;
        }
        return -1;
    }
}
