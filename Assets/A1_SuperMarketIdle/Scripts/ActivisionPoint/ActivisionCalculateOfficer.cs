using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivisionCalculateOfficer : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate;
    [SerializeField] List<GameObject> objectsToDeactivate = new List<GameObject>();
    [SerializeField] int totalInvestmentDuration, estimatedFPS;
    float nextInvestment = 0;
    public int totalInvestmentRequired, perInvestmentAmount;
    [SerializeField] TextMeshPro totalInvestmentRequiredText;
    [SerializeField] GameObject confettiExplosionPrefab, confettiItemStandOpeningPrefab;
    bool active = false;
    public MeshRenderer materialFillMeshRenderer;
    int totalInvestmentRequiredAtTheBeginning;
    [SerializeField] float investFrequency = 0, testProcent;

    [SerializeField] AudioSource audioSource;
    [SerializeField] List<GameObject> meshesToDisable = new List<GameObject>();

    private void Start()
    {
        totalInvestmentRequiredAtTheBeginning = totalInvestmentRequired;
        CalculateInvestFrequency();
        VisualProcess(totalInvestmentRequired);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            InvestmentCheck();
        }
    }
    void InvestmentCheck()
    {
        if (nextInvestment < Time.time && !active)
        {
            GetInvestment();
            nextInvestment = Time.time + investFrequency;
        }
    }
    void GetInvestment()
    {
        if (CheckIfPlayerHasEnoughMoney())
        {
            totalInvestmentRequired -= perInvestmentAmount;
            VisualProcess(totalInvestmentRequired);
            if (totalInvestmentRequired <= 0)
            {
                active = true;
                DeductFromPlayersMoney(perInvestmentAmount - totalInvestmentRequired);
                ActivateTheObject();
                if (objectToActivate.TryGetComponent<PortionActor>(out PortionActor pa))
                {
                    AdsManager.instance.adsActor.adsShowOfficer.ShowInterstitialAd(MyGoogleAdMob.AdPlacement.RoomExtend);
                }
                else if (objectToActivate.TryGetComponent<RoomActor>(out RoomActor ra))
                {
                    AdsManager.instance.adsActor.adsShowOfficer.ShowInterstitialAd(MyGoogleAdMob.AdPlacement.RoomAdd);
                }
                else if (objectToActivate.TryGetComponent<ItemStandActor>(out ItemStandActor ia))
                {
                    AdsManager.instance.adsActor.adsShowOfficer.ShowInterstitialAd(MyGoogleAdMob.AdPlacement.ItemStandOpen);
                }
                if (UIManager.instance.settingsMenuActor.soundState)
                {
                    audioSource.Play();
                }
            }
            else
            {
                DeductFromPlayersMoney(perInvestmentAmount);
            }
        }
        else if (PlayerManager.instance.playerCurrencyOfficer.Money > 0)
        {
            int playersMoney = PlayerManager.instance.playerCurrencyOfficer.Money;
            totalInvestmentRequired -= playersMoney;
            DeductFromPlayersMoney(playersMoney);
        }
    }

    public void VisualProcess(int _totalInvestmentRequired)
    {
        totalInvestmentRequiredText.text = _totalInvestmentRequired.ToString();
        float procent = 1 - (_totalInvestmentRequired / (float)totalInvestmentRequiredAtTheBeginning);
        testProcent = procent;
        MaterialFill(procent);
    }

    public void ActivateTheObject()
    {
        if (objectToActivate.activeSelf)
        {
            return;
        }
        objectToActivate.SetActive(true);
        DeactivateObjects();
        GameObject tempExplosionConfetti = Instantiate(confettiExplosionPrefab, transform.position, Quaternion.identity, objectToActivate.transform);
        if (objectToActivate.tag == "ItemStand")
        {
            GameObject tempconfettiItemStandOpening = Instantiate(confettiItemStandOpeningPrefab, objectToActivate.transform.position, confettiItemStandOpeningPrefab.transform.rotation, objectToActivate.transform);
            Destroy(tempconfettiItemStandOpening, 5f);
            //AdsManager.instance.adsActor.adsShowOfficer.ShowInterstitialAd(MyGoogleAdMob.AdPlacement.ItemStandOpen);
        }
        Destroy(tempExplosionConfetti, 5f);
        StartCoroutine(DestroyDelayer());
        //gameObject.SetActive(false);
    }

    IEnumerator DestroyDelayer()
    {
        foreach (GameObject meshObject in meshesToDisable)
        {
            meshObject.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject, 0.1f);
    }

    void DeductFromPlayersMoney(int deductAmount)
    {
        PlayerManager.instance.playerCurrencyOfficer.Money -= deductAmount;
        PlayerManager.instance.playerCurrencyOfficer.AddToInvestmentMoney(deductAmount);
    }

    bool CheckIfPlayerHasEnoughMoney()
    {
        bool hasEnoughMoney = (PlayerManager.instance.playerCurrencyOfficer.Money >= perInvestmentAmount) ? true : false;
        return hasEnoughMoney;
    }

    void DeactivateObjects()
    {
        foreach (GameObject item in objectsToDeactivate)
        {
            item.SetActive(false);
        }
    }

    void MaterialFill(float procent)
    {
        //print("MAT Name: " + materialFillMeshRenderer.materials[1].name+ " :: " + materialFillMeshRenderer.materials[1].HasProperty("_Process"));
        materialFillMeshRenderer.materials[1].SetFloat("_Process", procent);
        //materialFillMeshRenderer.materials[1].HasProperty("Process");
    }

    void CalculateInvestFrequency()
    {
        int investmentSteps = totalInvestmentDuration * estimatedFPS;
        //int investmentSteps = (totalInvestmentRequiredAtTheBeginning / perInvestmentAmount);
        perInvestmentAmount = (totalInvestmentRequiredAtTheBeginning / investmentSteps) < 1 ? 1 : (totalInvestmentRequiredAtTheBeginning / investmentSteps);
        investFrequency = totalInvestmentDuration / investmentSteps;
    }
}
