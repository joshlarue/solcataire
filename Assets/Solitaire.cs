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

    // Start is called before the first frame update
    void Start()
    {
        setupGame();
    }

    void setupGame()
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
            }
        }

        for (int i = cardIndex; i < deck.Count; i++)
        {
            GameObject card = Instantiate(deck[i], deckSpot.transform);
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
        
    }
}
