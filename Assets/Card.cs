using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit
{
    H,
    D,
    C,
    S
}

public enum Rank
{
    Ace,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King
}

public class Card : MonoBehaviour
{
    public Suit suit;
    public Rank rank;

    private SpriteRenderer front;
    private SpriteRenderer back;

    private void Awake()
    {
        front = transform.Find("Front").GetComponent<SpriteRenderer>();
        back = transform.Find("Back").GetComponent<SpriteRenderer>();
    }
    
    public void FlipFaceUp()
    {
        front.enabled = true;
        back.enabled = false;
        Debug.Log("Card flipped up: " + rank + suit);
    }

    public void FlipFaceDown()
    {
        front.enabled = false;
        back.enabled = true;
        Debug.Log("Card flipped down: " + rank + suit);
    }

    public void LoadCardSprite()
    {
        string spriteName = $"{RankToString(rank)}{suit}";
        Sprite cardSprite = Resources.Load<Sprite>(spriteName);

        if (cardSprite == null)
        {
            Debug.Log("Failed to load sprite: " + spriteName);
        }
        else
        {
            front.sprite = cardSprite;
        }
    }

    public string RankToString(Rank rank)
    {
        switch(rank)
        {
            case Rank.Ace: return "A";
            case Rank.Two: return "2";
            case Rank.Three: return "3";
            case Rank.Four: return "4";
            case Rank.Five: return "5";
            case Rank.Six: return "6";
            case Rank.Seven: return "7";
            case Rank.Eight: return "8";
            case Rank.Nine: return "9";
            case Rank.Ten: return "0";
            case Rank.Jack: return "J";
            case Rank.Queen: return "Q";
            case Rank.King: return "K";
            default: return "";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
