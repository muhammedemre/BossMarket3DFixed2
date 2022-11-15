using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SettingsMenuActor : MonoBehaviour
{
    [SerializeField] RectTransform music, sound, vibration;
    [SerializeField] RectTransform musicLeft, musicRight, soundLeft, soundRight, vibrationLeft, vibrationRight;
    public bool musicState, soundState, vibrationState;
    [SerializeField] float buttonMoveDuration;
    [SerializeField] Image musicBG, soundBG, vibrationBG;
    [SerializeField] Sprite onBackgroundSprite, offBackgroundSprite;
    [SerializeField] Sprite onSwitchSprite, offSwitchSprite;
    [SerializeField] GameObject musicOn, musicOff, soundOn, soundOff, vibrationOn, vibrationOff;
    [SerializeField] Color onColor, offColor;
    [SerializeField] AudioSource audioSource;

    public void PreActivePanel()
    {
        PrepareTheStateButton(musicBG, music, musicLeft, musicRight, musicState);
        PrepareTheStateButton(soundBG, sound, soundLeft, soundRight, soundState);
        PrepareTheStateButton(vibrationBG, vibration, vibrationLeft, vibrationRight, vibrationState);
    }

    public void ChangeMusicState()
    {
        musicState = !musicState;
        // PrepareTheButtonToTheNewState(musicBG, musicState, musicOn, musicOff);
        PrepareTheStateButton(musicBG, music, musicLeft, musicRight, musicState);
        // MoveButtonCircle(musicState, music, musicLeft, musicRight);
        if (UIManager.instance.settingsMenuActor.soundState)
        {
            audioSource.Play();
        }
        SoundManager.instance.MusicOnOff(musicState);
    }
    public void ChangeSoundState()
    {
        soundState = !soundState;
        // PrepareTheButtonToTheNewState(soundBG, soundState, soundOn, soundOff);
        PrepareTheStateButton(soundBG, sound, soundLeft, soundRight, soundState);
        // MoveButtonCircle(soundState, sound, soundLeft, soundRight);
        if (UIManager.instance.settingsMenuActor.soundState)
        {
            audioSource.Play();
        }
    }
    public void ChangeVibrationState()
    {
        vibrationState = !vibrationState;
        // PrepareTheButtonToTheNewState(vibrationBG, vibrationState, vibrationOn, vibrationOff);
        PrepareTheStateButton(vibrationBG, vibration, vibrationLeft, vibrationRight, vibrationState);
        // MoveButtonCircle(vibrationState, vibration, vibrationLeft, vibrationRight);
        if (UIManager.instance.settingsMenuActor.soundState)
        {
            audioSource.Play();
        }
    }

    void MoveButtonCircle(bool state, RectTransform buttonCircle, RectTransform leftRect, RectTransform rightRect)
    {
        RectTransform targetRect = state ? rightRect : leftRect;
        // Vector2 targetRectPos = CalculateAnchorPosition(targetRect);
        // buttonCircle.DOAnchorPos(targetRectPos, buttonMoveDuration);
        buttonCircle.DOMove(targetRect.transform.position, buttonMoveDuration);
        buttonCircle.GetComponent<Image>().sprite = state ? onSwitchSprite : offSwitchSprite;
    }

    Vector3 CalculateAnchorPosition(RectTransform rect)
    {
        float width = Screen.width;
        float height = Screen.height;

        float rectUMiddlePosRate = rect.anchorMin.x + ((rect.anchorMax.x - rect.anchorMin.x) / 2) - 0.5f;
        float rectYMiddlePosRate = rect.anchorMin.y + ((rect.anchorMax.y - rect.anchorMin.y) / 2) - 0.5f;
        Vector2 rectPos = new Vector3(width * rectUMiddlePosRate, height * rectYMiddlePosRate, 0f);
        return rectPos;
    }

    void PrepareTheButtonToTheNewState(Image button, bool state, GameObject onGameObject, GameObject offGameObject)
    {
        button.DOColor(state ? onColor : offColor, buttonMoveDuration);
        onGameObject.SetActive(state ? true : false);
        offGameObject.SetActive(state ? false : true);
    }

    void PrepareTheStateButton(Image button, RectTransform buttonCircle, RectTransform leftRect, RectTransform rightRect, bool state)
    {
        button.sprite = state ? onBackgroundSprite : offBackgroundSprite;
        buttonCircle.GetComponent<Image>().sprite = state ? onSwitchSprite : offSwitchSprite;
        MoveButtonCircle(state, buttonCircle, leftRect, rightRect);
    }
}
