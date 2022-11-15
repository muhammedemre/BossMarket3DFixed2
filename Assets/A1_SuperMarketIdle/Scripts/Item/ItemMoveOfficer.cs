using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemMoveOfficer : MonoBehaviour
{
    [SerializeField] ItemActor itemActor;
    public WareHouseOfficer relatedWareHouseOfficer;
    public Transform stackTarget;
    [SerializeField] float lerpDelay, yPadding;
    [SerializeField] float goStandDuration, itemStackReachTreshold, boxSoundPitchAddition;
    [SerializeField] bool reachedTheStack = false, onPlayer = false;
    [SerializeField] AudioSource audioSource;
    [SerializeField] int positionHeight;

    private void Update()
    {
        StackFollowCheck();
    }

    public void ItemTravelToTheItemTakePoint(Vector3 travelEndPosition, Vector3 travelEndEuler, float travelEulerRandomAngle, float travelDuration, float travelJumpPower)
    {

        float normalizedThrowDuration = Random.Range((travelDuration * 0.8f), (travelDuration * 1.2f));
        transform.DOJump(travelEndPosition, travelJumpPower, 1, normalizedThrowDuration);


        float normalizedEulerY = Random.Range(travelEndEuler.y - travelEulerRandomAngle, travelEndEuler.y + travelEulerRandomAngle);
        Vector3 normalizedEuler = new Vector3(travelEndEuler.x, normalizedEulerY, travelEndEuler.z);
        transform.DORotate(normalizedEuler, travelDuration);
    }


    public void PoolItself()
    {
        StartCoroutine(PoolDelay());
    }

    IEnumerator PoolDelay()
    {
        yield return new WaitForSeconds(0.1f);
        itemActor.modelOfficer.SelectTheModel(0);
        relatedWareHouseOfficer.usedItemList.Remove(transform);
        relatedWareHouseOfficer.itemPoolList.Add(transform);
        transform.position = relatedWareHouseOfficer.transform.position;
        gameObject.SetActive(false);
    }

    void StackFollowCheck()
    {
        if (stackTarget != null)
        {
            Vector3 stackTargetNewPos = stackTarget.position + new Vector3(0f, yPadding, 0f);
            transform.position = Vector3.Lerp(transform.position, stackTargetNewPos, lerpDelay);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, stackTarget.eulerAngles, lerpDelay);
            CheckIfReachedItemStackPos();
        }
    }
    public void FollowTheStackTarget(Transform _stackTarget, float heightEffect, int _positionHeight, bool _onPlayer)
    {
        stackTarget = _stackTarget;
        lerpDelay = heightEffect;
        positionHeight = _positionHeight;
        onPlayer = _onPlayer;
    }

    public void ItemGoStand(Transform standPosition, ParticleSystem particle)
    {
        transform.DOMove(standPosition.position, goStandDuration).OnComplete(() => particle.Play());
        transform.DORotate(standPosition.eulerAngles, goStandDuration);
        //transform.DORotate(new Vector3(-13.5f, 0, 0f), goStandDuration);
    }
    public void ItemGoShoppingBag(Transform bagPosition)
    {
        transform.DOMove(bagPosition.position, goStandDuration).OnComplete(() => PoolItself());
    }

    void CheckIfReachedItemStackPos()
    {
        if (itemStackReachTreshold > Vector3.Distance(transform.position, stackTarget.position) && !reachedTheStack && onPlayer)
        {
            Debug.Log("CheckIfReachedItemStackPos");
            reachedTheStack = true;
            audioSource.pitch = (0.5f + CalculatePitch(positionHeight));
            if (UIManager.instance.settingsMenuActor.soundState)
            {
                audioSource.Play();
            }
        }
    }

    float CalculatePitch(int height)
    {
        return boxSoundPitchAddition * height;
    }
}
