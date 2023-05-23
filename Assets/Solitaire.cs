using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solitaire : MonoBehaviour
{
    public GameObject[] cards;

    private List<GameObject> deck;

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

        for (int i = 0; i < deck.Count; i++)
        {
            GameObject temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        int cardIndex = 0;
        for (int i = 0; i < stacks.Length; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                GameObject card = Instantiate(deck[cardIndex], stacks[i].transform.position - new Vector3(0, j * offset, j * offset), Quaternion.identity);
                
                cardIndex++;

                if (j == i)
                {
                    card.GetComponent<Card>().FlipFaceUp();
                }
                yield return new WaitForSeconds(dealDelay);
            }
        }

        for (int i = cardIndex; i < deck.Count; i++)
        {
            GameObject card = Instantiate(deck[i], deckSpot.transform);
            yield return new WaitForSeconds(dealDelay);
        }

        RemoveOriginalCards();
    }

    void RemoveOriginalCards()
    {
        foreach (GameObject card in cards)
        {
            Destroy(card);
        }
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
                    cardInteractionLogic();
                    Debug.Log("touched card: " + hitCard.name);
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
                    cardInteractionLogic();
                    Debug.Log("clicked card: " + hitCard.suit + hitCard.rank);
                }
            }
        }
        #endif
    }

    void cardInteractionLogic()
    {

    }
}
