using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
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
    
    public void FlipFaceUp()
    {

    }

    public void FlipFaceDown()
    {

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
