using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the core logic of the memory match game.
/// Manages board creation, card interaction, scoring, timing, and game state.
/// </summary>
public class MemoryGameController : MonoBehaviour
{
    public AdjustGridCellSize adjustGridCellSize;
    public GameSave gameSave;

    public GameObject cardPrefab;
    public GameInfo gameInfo;

    private List<CardScript> allCard = new List<CardScript>();
    public List<Sprite> suffledSpriteList { get; private set; }
    public List<bool> CardFlippedData { get; private set; }

    private bool firstClick, secoundClick;
    private CardScript firstClickCard, secoundClickCard;
    

    public bool isGameCompleted {  get; private set; }
    public int currentScore { get; private set; }
    public float currentTimer { get; private set; }
    public int countCorrectCardFlip { get; private set; }

    private float comboTimer;
    private int comboCount=1;

    private bool isComboTimerStart;

    
    void Start()
    {
        if (PlayerPrefsHalper.IS_GAME_SAVED)
        {
            CreateSavedBoard();
            currentTimer = PlayerPrefsHalper.SAVED_GAME_TIMER;
            currentScore = PlayerPrefsHalper.SAVED_GAME_CURRENT_SCORE;
            countCorrectCardFlip = PlayerPrefsHalper.COUNT_CORRECT_CARD_FLIP;
        }
        else
        {
            CreateBoard();
            currentTimer = gameInfo.GamePlayTime;
            GamePlayManager.Instance.ScoreUpdate(currentScore);
        }
    }


    private void Update()
    {
       
        if (isGameCompleted)
        {
            return;
        }
        ComboTimer();
        GameTimerLogic();

    }

    // Combo Timer Logic
    /// <summary>
    /// Updates the combo timer and resets the combo count if the timer expires.
    /// </summary>
    private void ComboTimer()
    {
        if (isComboTimerStart)
        {
            comboTimer += Time.deltaTime;
            GamePlayManager.Instance.ComboTimerUpdate(comboCount * 2,
                1-(comboTimer / gameInfo.ComboResetTimer));
            if (comboTimer >= gameInfo.ComboResetTimer)
            {
                isComboTimerStart = false;
                comboCount = 1;
                comboTimer = 0;
                GamePlayManager.Instance.ComboObjectActivate(false);
            }
        }
    }

    //Game Play Timer Logic
    //cuurentTime value get from GameInfo scriptable object.
    /// <summary>
    /// Updates the main game timer and checks for level failure (time out).
    /// </summary>
    private void GameTimerLogic()
    {
        currentTimer -= Time.deltaTime;
        GamePlayManager.Instance.TimerUpdate(TextFormate.getSecToMinNano(currentTimer));
        if (currentTimer < 0)
        {
            GamePlayManager.Instance.LevelFailed();
            isGameCompleted = true;
        }
    }

    //NEW Board Creation
    //Card instance through this Function based on ROW and COLUMN value.
    /// <summary>
    /// Creates a new game board by instantiating cards and shuffling sprites.
    /// </summary>
    private void CreateBoard()
    {
        int rowCount = PlayerPrefsHalper.ROW_COUNT;
        int columnCount = PlayerPrefsHalper.COLUMN_COUNT;
        int totalCardCount = rowCount * columnCount;
       
        suffledSpriteList = CreateSuffuleList(totalCardCount);
        CardFlippedData = new List<bool>();
        for (int i = 0; i < totalCardCount; i++)
        {
            GameObject card = Instantiate(cardPrefab);
            card.transform.SetParent(transform, false);
            card.name = "Card " + i;

            CardScript cardScript = card.GetComponent<CardScript>();
            allCard.Add(cardScript);
            CardFlippedData.Add(false);
            cardScript.cardButton.onClick.AddListener(() => CardSelect(cardScript));

            
            cardScript.mainSprite = suffledSpriteList[i];
            cardScript.CardID = int.Parse(suffledSpriteList[i].name);
        }
        adjustGridCellSize.UpdateCellSize(columnCount,rowCount);
    }

    // OLD Board Creation based on saved value at PlayerPrefs
    /// <summary>
    /// Recreates the game board from saved data, restoring card states.
    /// </summary>
    private void CreateSavedBoard()
    {
        int rowCount = PlayerPrefsHalper.ROW_COUNT;
        int columnCount = PlayerPrefsHalper.COLUMN_COUNT;
        int totalCardCount = rowCount * columnCount;

        suffledSpriteList = gameSave.GetSuffuleList();
        CardFlippedData = PlayerPrefsHalper.SAVED_GAME_CARD_FLIPPED_DATA;
        for (int i = 0; i < totalCardCount; i++)
        {
            GameObject card = Instantiate(cardPrefab);
            card.transform.SetParent(transform, false);
            card.name = "Card " + i;

            CardScript cardScript = card.GetComponent<CardScript>();
            allCard.Add(cardScript);
            cardScript.cardButton.onClick.AddListener(() => CardSelect(cardScript));


            cardScript.mainSprite = suffledSpriteList[i];
            cardScript.CardID = int.Parse(suffledSpriteList[i].name);
           
        }
        adjustGridCellSize.UpdateCellSize(columnCount, rowCount);

        StartCoroutine(GameManager.Instance.ActionCallAfterTime(.03f, true, () => {
            for (int i = 0; i < totalCardCount; i++)
            {
                allCard[i].gameObject.SetActive(!CardFlippedData[i]);
            }
        }));
       
    }

    //Suffling the Sprites.
    /// <summary>
    /// Creates a list of paired sprites and shuffles them for the board.
    /// </summary>
    private List<Sprite> CreateSuffuleList(int totalCardCount)
    {
        List<Sprite> allSprites = new List<Sprite>();
        for(int i = 0; i < totalCardCount/2; i++)
        {
            allSprites.Add(gameInfo.allCardSprites[i]);
            allSprites.Add(gameInfo.allCardSprites[i]);
        }
       for(int i = 0; i < allSprites.Count; i++)
        {
            Sprite temp = allSprites[i];
            int randomNumber = Random.Range(i, allSprites.Count);
            allSprites[i] = allSprites[randomNumber];
            allSprites[randomNumber] = temp;
        }
        return allSprites;
    }

    //All Card Listener connected to this function.
    //Mange user click first time and secound time.
    /// <summary>
    /// Handles card selection input. Manages first and second card clicks.
    /// </summary>
    public void CardSelect(CardScript cardScript)
    {
        if (!firstClick)
        {
            firstClick = true;
            firstClickCard = cardScript;

            //CardView have all functionality to mange animation of fliping the Card
            cardScript.CardView();
        }
        else if (!secoundClick && firstClickCard!=cardScript)
        {
            secoundClick = true;
            secoundClickCard = cardScript;
    
            cardScript.CardView();
            StartCoroutine(CheckCardMatch());
        }
    }

    //Based on user two choies this function calculate the right/wrong gusses.
    //Score calculation.
    //Combo initiated
    /// <summary>
    /// Coroutine to check if the two selected cards match.
    /// Handles scoring, combos, and card states (match/mismatch).
    /// </summary>
    IEnumerator CheckCardMatch()
    {
        yield return new WaitForSeconds(.5f);
        if(firstClickCard.CardID == secoundClickCard.CardID)
        {
            if (isComboTimerStart)
            {
                comboCount *= 2;
                comboTimer = 0;
            }
            else
            {
                isComboTimerStart = true;
            }
            GamePlayManager.Instance.scoreEffect.SetScoreEffect((comboCount * gameInfo.ScoreMultiplier), firstClickCard.transform.position);
            currentScore =  currentScore + (comboCount*gameInfo.ScoreMultiplier);


            //Card Match have functionality to mange Animation and its disable the selected cards.
            firstClickCard.CardMatch();
            secoundClickCard.CardMatch();

            //This data manage for Save Game Logic.
            CardFlippedData[allCard.IndexOf(firstClickCard)] = true;
            CardFlippedData[allCard.IndexOf(secoundClickCard)] = true;

            GamePlayManager.Instance.ScoreUpdate(currentScore);
            CheckGameFinished();
        }
        else
        {
            //Reset the button when user choise wrong
          
            firstClickCard.CardNotMatch();
            secoundClickCard.CardNotMatch();

            if (isComboTimerStart)
            {
                isComboTimerStart = false;
                comboCount = 1;
                comboTimer = 0;
                GamePlayManager.Instance.ComboObjectActivate(false);
            }
        }
        firstClick = false;
        secoundClick = false;
    }
    
    /// <summary>
    /// Checks if all card pairs have been found to complete the level.
    /// </summary>
    private void CheckGameFinished()
    {
        countCorrectCardFlip++;
        if(countCorrectCardFlip== allCard.Count / 2)
        {
            StartCoroutine(GameManager.Instance.ActionCallAfterTime(.5f, true, () => {
                GamePlayManager.Instance.LevelCompleted(currentScore);
            }));
            isGameCompleted = true;
        }
    }
}
