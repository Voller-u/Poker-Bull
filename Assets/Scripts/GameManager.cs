using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("扑克牌牌面")]
    public List<Sprite> cardsImage = new();
    private int remainCardNum;
    [Header("牌桌")]
    public GameObject deck;
    public TextMeshProUGUI deckCardNum;

    public GameObject playerCardsOrder;
    public GameObject systemCardsOrder;

    private int[,] cardOrder = { {1,2,3,4,5 },{1,2,4,3,5 },{1,2,5,3,4 },{1,3,4,2,5 },{1,3,5,2,4 },
    {1,4,5,2,3 },{2,3,4,1,5},{2,3,5,1,4 },{2,4,5,1,3},{3,4,5,1,2} };

    private List<List<int>> expectCardNum = new();

    public GameObject SystemCards;
    public GameObject PlayerCards;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }

    void Start()
    {
        GameStart();
        TurnStart();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Eval(List<int> cards)
    {
        //炸弹
        int[] numCnt = new int[14];
        foreach (int card in cards)
        {
            numCnt[card]++;
        }
        for (int i = 1; i <= 13; i++)
            if (numCnt[i] >= 4) return 15;

        //五小
        int numSum = 0;
        foreach (int card in cards)
            numSum += card;
        if (numSum <= 10) return 14;

        //金牛
        bool goldBull = true;
        foreach (int card in cards)
            if (card <= 10) goldBull = false;
        if (goldBull)
            return 13;

        //银牛
        bool silverBull = true;
        foreach (int card in cards)
            if (card < 10) silverBull = false;
        if (silverBull)
            return 12;

        if (cards[0] + cards[1] + cards[2] % 10 == 0)
        {
            int score = (cards[3] % 10 + cards[4] % 10) % 10;
            return score;
        }

        return 0;
    }

    //根据五张牌返回应该如何组合成三加二的形式
    public int Evaluate(List<Card> cards)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (expectCardNum[i].Count != 5)
                    expectCardNum[i].Add(cards[cardOrder[i, j]].cardNum);
                else
                    expectCardNum[i][j] = cards[cardOrder[i, j]].cardNum;
            }
        }

        int maxValue = 0 - int.MaxValue;
        int order = 0;
        for (int i = 0; i < 10; i++)
        {
            int val = Eval(expectCardNum[i]);
            if (val > maxValue)
            {
                maxValue = val;
                order = i;
            }
        }
        return order;
    }
    public int CardImageToCardNum(Sprite cardImage)
    {
        string card = cardImage.name;
        Match match = Regex.Match(card, @"\d+$");
        if (match.Success)
        {
            int cardNum = int.Parse(match.Value);
            return (cardNum + 1) % 13 == 0 ? 13 : (cardNum + 1) % 13;
        }
        return -1;
    }

    public void GameStart()
    {
        remainCardNum = 52;
        Utils.Shuffle<Sprite>(cardsImage);
    }

    public void TurnStart()
    {
        //开始发牌
        if (remainCardNum >= 12)
        {
            Player player = PlayerCards.GetComponent<Player>();
            for (int i = 0; i < player.cards.Count; i++)
            {
                player.cards[i].SetImage(--remainCardNum);
                player.cards[i].cardNum = CardImageToCardNum(cardsImage[remainCardNum]);
            }
            AI ai = SystemCards.GetComponent<AI>();
            for (int i = 0; i < ai.cards.Count; i++)
            {
                string num = cardsImage[--remainCardNum].name;
                Match match = Regex.Match(num, @"\d+$");
                ai.cards[i].spriteNum = int.Parse(match.Value);
                ai.cards[i].cardNum = CardImageToCardNum(cardsImage[remainCardNum]);
                
            }
            deckCardNum.text = remainCardNum.ToString();
        }

    }

    public void TurnOver()
    {
        //比点数
        deck.SetActive(false);
        playerCardsOrder.SetActive(true);
        systemCardsOrder.SetActive(true);
    }
}
