using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdjustGridCellSize))]
public class MemoryGameController : MonoBehaviour
{
    
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameInfo gameInfo;

    private AdjustGridCellSize _adjustGridCellSize;
    private AdjustGridCellSize adjustGridCellSize{
        get { 
            if(_adjustGridCellSize==null)
                _adjustGridCellSize = GetComponent<AdjustGridCellSize>();
            return _adjustGridCellSize; 
            } 
        }
    private List<CardScript> allCards = new List<CardScript>();

    private bool firstClick, secondClick;
    private CardScript firstClickCard, secondClickCard;

    private int currentScore;
    private float comboTimer;
    private int comboCount=1;
    private bool isComboActive;

    private int matchedPairs;
    void Start()
    {
        CreateBoard();
    }
    private void Update()
    {
        if (isComboActive)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer >= gameInfo.ComboResetTimer)
            {
                isComboActive = false;
                comboCount = 1;
                comboTimer = 0;
            }
        }
    }
    private void CreateBoard()
    {
        int rowCount = PlayerPrefsHalper.ROW_COUNT;
        int columnCount = PlayerPrefsHalper.COLUMN_COUNT;
        int totalCardCount = rowCount * columnCount;
        
        
        // Create a list of indices (pairs) and shuffle them
        List<int> shuffledIndices = CreateShuffleList(totalCardCount);

        for (int i = 0; i < totalCardCount; i++)
        {
            
            GameObject card = Instantiate(cardPrefab);
            card.transform.SetParent(transform, false);
            card.name = "Card " + i;

            CardScript cardScript = card.GetComponent<CardScript>();
            allCards.Add(cardScript);
            cardScript.cardButton.onClick.AddListener(() => OnCardSelected(cardScript));

            // Assign sprite and ID based on the shuffled index
            int dataIndex = shuffledIndices[i];
            cardScript.mainSprite = gameInfo.allCardSprites[dataIndex];
            cardScript.CardID = dataIndex;
        }
        adjustGridCellSize.UpdateCellSize(columnCount,rowCount);
    }

    private List<int> CreateShuffleList(int totalCardCount)
    {
        List<int> idList = new List<int>();
        // Add pairs of IDs
        for(int i = 0; i < totalCardCount/2; i++)
        {
            idList.Add(i);
            idList.Add(i);
        }
        
        // Fisher-Yates shuffle
       for(int i = 0; i < idList.Count; i++)
        {
            int temp = idList[i];
            int randomNumber = Random.Range(i, idList.Count);
            idList[i] = idList[randomNumber];
            idList[randomNumber] = temp;
        }
        return idList;
    }

    public void OnCardSelected(CardScript cardScript)
    {
        // Ignore input if we are already checking a match or clicked the same card
        if (secondClick || cardScript == firstClickCard) return;

        if (!firstClick)
        {
            firstClick = true;
            firstClickCard = cardScript;
            cardScript.cardImage.sprite = cardScript.mainSprite;
        }
        else if (!secondClick)
        {
            secondClick = true;
            secondClickCard = cardScript;
            cardScript.cardImage.sprite = cardScript.mainSprite;

            StartCoroutine(CheckCardMatch());
        }
    }
    IEnumerator CheckCardMatch()
    {
        yield return new WaitForSeconds(1f);
        
        if(firstClickCard.CardID == secondClickCard.CardID)
        {
            // Match found
            if (isComboActive)
            {
                comboCount *= 2;
                comboTimer = 0;
            }
            else
            {
                isComboActive = true;
                // TODO: Trigger combo UI/Animation here
            }
            
            currentScore += comboCount;
            
            // Disable matched cards
            firstClickCard.gameObject.SetActive(false);
            secondClickCard.gameObject.SetActive(false);

            CheckGameFinished();
        }
        else
        {
            // No match, reset sprites
            firstClickCard.cardImage.sprite = firstClickCard.backgroundSprite;
            secondClickCard.cardImage.sprite = secondClickCard.backgroundSprite;
        }
        
        // Reset turn
        firstClick = false;
        secondClick = false;
        firstClickCard = null;
        secondClickCard = null;
    }
    void CheckGameFinished()
    {
        matchedPairs++;
        if(matchedPairs == allCards.Count / 2)
        {
            Debug.Log("Game Completed. Final Score: " + currentScore);
        }
    }
}
