using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solitaire : MonoBehaviour
{
    public GameObject[] cards;
    public GameObject CardTemplate;
    public Sprite backSprite;
    public List<Sprite> cardSprites = new List<Sprite>();

    private List<GameObject> deck;

    public Card card;

    public GameObject[] stacks;
    public GameObject[] suits;
    public GameObject deckSpot;
    public float offset;
    public float dealDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(setupGame());
    }

    IEnumerator setupGame()
    {
        deck = new List<GameObject>(cards);
        List<GameObject> createdCards = new List<GameObject>();

        // Create and store cards
        for (int i = 0; i < cardSprites.Count; i++)
        {
            GameObject card = Instantiate(CardTemplate, deckSpot.transform);
            card.transform.position = deckSpot.transform.position;//= new Vector3(card.transform.position.x, card.transform.position.y - offset * i, card.transform.position.z);
            
            Card cardScript = card.GetComponent<Card>();
            cardScript.rank = (Rank)(i / 4);
            cardScript.suit = (Suit)(i % 4);

            SpriteRenderer front = card.transform.Find("Front").GetComponent<SpriteRenderer>();
            front.sprite = cardSprites[i];
            SpriteRenderer back = card.transform.Find("Back").GetComponent<SpriteRenderer>();
            back.sprite = backSprite;

            cardScript.LoadCardSprite();
            createdCards.Add(card);

            yield return new WaitForSeconds(dealDelay);
        }

        // Shuffle createdCards
        for (int i = 0; i < createdCards.Count; i++)
        {
            GameObject temp = createdCards[i];
            int randomIndex = Random.Range(i, createdCards.Count);
            createdCards[i] = createdCards[randomIndex];
            createdCards[randomIndex] = temp;
        }

        int cardIndex = 0;
        for (int i = 0; i < stacks.Length; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                GameObject card = createdCards[cardIndex++];
                card.transform.position = stacks[i].transform.position - new Vector3(0, j * offset, j);
                //card.transform.SetSiblingIndex(j);

                //cardIndex++;

                if (j == i)
                {
                    card.GetComponent<Card>().FlipFaceUp();
                }
                else
                {
                    card.GetComponent<Card>().FlipFaceDown();
                }
                yield return new WaitForSeconds(dealDelay);
            }
        }
        Destroy(CardTemplate);
        Debug.Log(deck.Count);

    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            RaycastHit2d hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
            if (hit.collider != null)
            {
                Card hitCard = hit.collider.GetComponent<Card>();
                if (hitCard != null)
                {
                    //CardTemplate.GetComponent<Card>().cardInteractionLogic();
                    //Debug.Log("touched card: " + hitCard.name);
                }
            }
        }
        #else
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Card hitCard = hit.collider.GetComponent<Card>();
                if (hitCard != null)
                {
                    //CardTemplate.GetComponent<Card>().cardInteractionLogic();
                    //Debug.Log("clicked card: " + hitCard.suit + hitCard.rank);
                }
            }
        }
        #endif
    }
}
