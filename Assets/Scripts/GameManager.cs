using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    [Header("ÆË¿ËÅÆÅÆÃæ")]
    public List<Sprite> cardsImage = new();

    private void Awake()
    {
        
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
