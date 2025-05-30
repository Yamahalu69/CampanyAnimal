using UnityEngine;
using UnityEngine.UI;

public class SwitchSprite : MonoBehaviour
{
    public Sprite upArrow, downArrow, rightArrow, leftArrow, endOfTask;

    private Image image;

    public void SwitchSpriteTo(KeyCode key)
    {
        image = GetComponent<Image>();

        switch (key)
        {
            case KeyCode.LeftArrow:
                image.sprite = leftArrow;
                break;
            case KeyCode.RightArrow:
                image.sprite = rightArrow;
                break;
            case KeyCode.UpArrow:
                image.sprite = upArrow;
                break;
            case KeyCode.DownArrow:
                image.sprite = downArrow;
                break;
            default:
                image.sprite = endOfTask;
                break;
        }
    }
}
