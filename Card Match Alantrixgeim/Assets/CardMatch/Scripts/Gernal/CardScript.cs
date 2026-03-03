using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Controls the behavior of a single card in the memory game.
/// Handles flipping animations, matching logic, and audio playback.
/// </summary>
public class CardScript : MonoBehaviour
{
    public Button cardButton;
    public Image cardImage;
    public Sprite backgroundSprite;
    public Sprite mainSprite;
    public int CardID;


    public Animator cardAnimator;

    [Header("Audio Clips")]
    public AudioClip cardFlipClip;
    public AudioClip cardNotMatchClip;
    public AudioClip cardMatchClip;


    private bool isCardView;

    /// <summary>
    /// Initiates the card flip animation to show the card face.
    /// </summary>
    public void CardView()
    {
        isCardView = true;
        cardAnimator.Play("CardFlipAnimation", -1);
    }

    /// <summary>
    /// Handles the logic when a card is successfully matched.
    /// Plays the match animation, sound, and hides the card after a delay.
    /// </summary>
    public void CardMatch()
    {
        cardAnimator.Play("CardMatch", -1);
        Events.playAudio(cardMatchClip);
        StartCoroutine(GameManager.Instance.ActionCallAfterTime(.4f, true, () => {
            gameObject.SetActive(false);
        }));
    }

    /// <summary>
    /// Handles the logic when a card does not match the other selected card.
    /// Plays the mismatch animation and flips the card back over.
    /// </summary>
    public void CardNotMatch()
    {
        cardAnimator.Play("CardNotMatch", -1);
        Events.playAudio(cardNotMatchClip);
        isCardView = false;
    }

    /// <summary>
    /// Called via an Animation Event during the flip animation.
    /// Swaps the sprite between the background (back) and main sprite (face).
    /// </summary>
    public void CardFlipAnimationEvent()
    {
        if (isCardView)
        {
            cardImage.sprite = mainSprite;
        }
        else
        {
            cardImage.sprite = backgroundSprite;
        }
        Events.playAudio(cardFlipClip);
    }
}
