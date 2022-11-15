using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierWorkerActor : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] float liveDuration;
    float deathTime;

    public void StartProcess(float _liveDuration)
    {
        liveDuration = _liveDuration;
        particle.Play();
        deathTime = Time.time + liveDuration;
    }

    private void Update()
    {
        LiveCheck();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CashierDesk")
        {
            other.GetComponent<RootFinderOfficer>().root.GetComponent<CashierActor>().moneyHandleOfficer.GetTheMoneyOnTheDesk();
        }
    }

    void LiveCheck() 
    {
        if (deathTime < Time.time)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die() 
    {
        particle.Play();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
