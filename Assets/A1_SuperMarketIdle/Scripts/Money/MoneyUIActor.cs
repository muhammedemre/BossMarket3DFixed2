using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lofelt.NiceVibrations;

public class MoneyUIActor : MonoBehaviour
{
    public MoneyStackOfficer relatedMoneyStackOfficer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] HapticSource audioHapticSource;
    [SerializeField] List<float> moneySoundRandomRange = new List<float>();

    public void MoneyTravelToMoneyUIOnCanvas(Vector2 travelStartPosition, Vector3 travelEndPosition, float travelDuration)
    {
        //transform.position = travelStartPosition;
        gameObject.GetComponent<RectTransform>().anchoredPosition = travelStartPosition;
        float normalizedThrowDuration = Random.Range((travelDuration * 0.8f), (travelDuration * 1.2f));
        //transform.DOMove(travelEndPosition, travelDuration).OnComplete(()=> PoolItself());
        Transform _image = transform.GetChild(0);
        _image.DOScale(Vector3.one, travelDuration);
        // gameObject.GetComponent<RectTransform>().DOAnchorPos(travelEndPosition, travelDuration).SetEase(Ease.InBack).OnComplete(() => GiveTheMoney());
        gameObject.GetComponent<RectTransform>().DOMove(travelEndPosition, travelDuration).SetEase(Ease.InBack).OnComplete(() => GiveTheMoney());
    }

    void GiveTheMoney()
    {
        int valueOfTheBanknote = relatedMoneyStackOfficer.cashierActor.belongingRoom.banknoteValue;
        PlayerManager.instance.playerCurrencyOfficer.MoneyDepositToTheWallet(valueOfTheBanknote);
        PoolItself();
    }

    void PoolItself()
    {
        transform.GetChild(0).localScale = new Vector3(0.55f, 0.55f, 0.55f); // reScale the money Image
        relatedMoneyStackOfficer.usedMoneyUIList.Remove(transform);
        relatedMoneyStackOfficer.pooledMoneyUIList.Add(transform);
        transform.position = relatedMoneyStackOfficer.moneyUIPoolingPosition + new Vector3(-1000f, 0f, 0f);
        float volume = UnityEngine.Random.Range(moneySoundRandomRange[0], moneySoundRandomRange[1]);

        if (UIManager.instance.settingsMenuActor.soundState)
        {
            audioSource.volume = volume;
            audioSource.Play();
        }
        //audioHapticSource.Play();
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
