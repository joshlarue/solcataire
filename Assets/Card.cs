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
    public float offset;
    private Vector3 originalPosition;
    private Vector3 mousePosition;
    private bool isHeld;
    private Card collidingCard;

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

    void OnMouseDown()
    {
        originalPosition = this.transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                isHeld = true;
                Debug.Log($"Picked up {rank} of {suit}");
            }
        }
    }

    private Card otherCard;
    void OnMouseUp()
    {
        isHeld = false;
        if (cardInteractionLogic()) // if cardlogic returns true
        {
            Debug.Log("CARDLOGIC TRUE");

            if (collidingCard != null)
            { 
                this.transform.position = collidingCard.transform.position - new Vector3(0, offset, 0);
                front.sortingOrder = 999;
            }
            else
            {
                this.transform.position = originalPosition;
            }
        }
        else
        {
            Debug.Log("CARDLOGIC FALSE");
            this.transform.position = originalPosition;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        otherCard = collision.gameObject.GetComponent<Card>();
        Debug.Log($"collision detected with {otherCard}");
        if (otherCard != null)
        {
            collidingCard = otherCard;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        otherCard = collision.gameObject.GetComponent<Card>();
        Debug.Log($"collision exited with {otherCard}");
        if (otherCard != null)
        {
            collidingCard = null;
        }
    }
    bool cardInteractionLogic()
    {
        if (collidingCard != null)
        {
            //Vector3 newPosition = collidingCard.transform.position;
            //newPosition.y -= offset;
            //this.transform.position = newPosition;            
            Debug.Log("position set to other card");
            return true;
        }
        else
        {
            Debug.Log("card null");
            return false;
            //Debug.Log("null card");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 50;
            this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
            front.sortingOrder = 1000;
        }
        else
        {
            //front.sortingOrder = 0;
        }
    }
}
