using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoneyActor : MonoBehaviour
{
    public MoneyStackOfficer relatedMoneyStackOfficer;
    public void MoneyTravelToTheDesk(Vector3 throwStartPosition, Vector3 throwEndPosition, Vector3 throwEndEuler, float throwEulerRandomAngle, float throwDuration, float throwJumpPower, bool isStackFull, List<Transform> listToBeAttached)
    {
        transform.position = throwStartPosition;

        float normalizedThrowDuration = Random.Range((throwDuration * 0.8f), (throwDuration * 1.2f));

        if (!isStackFull)
        {
            transform.DOJump(throwEndPosition, throwJumpPower, 1, normalizedThrowDuration).OnComplete(() => AddToUsedMoneyList(listToBeAttached, isStackFull));
        }
        else
        {
            transform.DOJump(throwEndPosition, throwJumpPower, 1, normalizedThrowDuration).OnComplete(() => AddToUsedMoneyList(listToBeAttached, isStackFull));//.OnComplete(() => PoolItself());
        }


        float normalizedEulerY = Random.Range(throwEndEuler.y - throwEulerRandomAngle, throwEndEuler.y + throwEulerRandomAngle);
        Vector3 normalizedEuler = new Vector3(throwEndEuler.x, normalizedEulerY, throwEndEuler.z);
        transform.DORotate(normalizedEuler, throwDuration);
    }

    void AddToUsedMoneyList(List<Transform> listToBeAttached, bool isStackFull)
    {
        listToBeAttached.Add(transform);
        if (isStackFull)
        {
            PoolItself();
        }
    }
    public void PoolItself()
    {
        relatedMoneyStackOfficer.usedMoneyList.Remove(transform);
        relatedMoneyStackOfficer.pooledMoneyList.Add(transform);
        transform.position = relatedMoneyStackOfficer.moneyPoolingPosition;
        gameObject.SetActive(false);
    }
}
