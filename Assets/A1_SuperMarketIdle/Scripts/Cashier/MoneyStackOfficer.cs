using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MoneyStackOfficer : MonoBehaviour
{
    public CashierActor cashierActor;
    [SerializeField] Transform moneyStackPositions, moneyPool, moneyPrefab, testThrowPosition, moneyUIPoolPrefab, moneyUIPrefab;
    [SerializeField] float throwRandomAngle, throwDuration, throwDeskJumpPower, moneyUITravelDuration;
    public Vector3 moneyPoolingPosition, moneyUIPoolingPosition;
    [SerializeField] int extraMoneyForPool; // when stack is full but customers still pay, we need this extra.

    public List<Transform> pooledMoneyList = new List<Transform>();
    public List<Transform> usedMoneyList = new List<Transform>();

    public List<Transform> pooledMoneyUIList = new List<Transform>();
    public List<Transform> usedMoneyUIList = new List<Transform>();

    Transform moneyUIPool;
    public int thrownMoneyCounter = 0;

    bool moneyCollectIsBusy = false;

    private void Awake()
    {
        PrepareTheMoneyPool();
        PrepareTheMoneyUIPool();
    }

    void PrepareTheMoneyPool()
    {
        int moneyCounter = 0;
        for (int i = 0; i < moneyStackPositions.childCount + extraMoneyForPool; i++)
        {
            Transform tempMoney = Instantiate(moneyPrefab, moneyPoolingPosition, Quaternion.identity, moneyPool);
            tempMoney.name = "Money_" + moneyCounter.ToString();
            tempMoney.GetComponent<MoneyActor>().relatedMoneyStackOfficer = this;
            pooledMoneyList.Add(tempMoney);
            tempMoney.gameObject.SetActive(false);
            moneyCounter++;
        }
    }

    void PrepareTheMoneyUIPool()
    {

        moneyUIPool = Instantiate(moneyUIPoolPrefab, UIManager.instance.InGameMoney.transform.position, Quaternion.identity, UIManager.instance.InGameMoney.transform);
        moneyUIPool.name = "MoneyUIPool_" + this.name;

        int moneyUICounter = 0;
        for (int i = 0; i < pooledMoneyList.Count * 2; i++)
        {
            Transform tempUIMoney = Instantiate(moneyUIPrefab, moneyUIPool.position, Quaternion.identity, moneyUIPool);
            tempUIMoney.GetComponent<MoneyUIActor>().relatedMoneyStackOfficer = this;
            tempUIMoney.gameObject.SetActive(false);
            tempUIMoney.name = "MoneyUI_" + moneyUICounter.ToString();

            pooledMoneyUIList.Add(tempUIMoney);
            moneyUICounter++;
        }
    }

    public IEnumerator ThrowMoney(int amount, Vector3 throwPosition, float stackingFrequency)
    {
        for (int i = 0; i < amount; i++)
        {
            Transform moneyToThrow = GetPoolObjectFromThePool(pooledMoneyList);
            //int stackIndex = (usedMoneyList.Count < moneyStackPositions.childCount) ? usedMoneyList.Count-1 : moneyStackPositions.childCount-1;
            int stackIndex = (thrownMoneyCounter < moneyStackPositions.childCount) ? thrownMoneyCounter : moneyStackPositions.childCount - 1;
            Transform throwTarget = moneyStackPositions.GetChild(stackIndex);
            moneyToThrow.GetComponent<MoneyActor>().MoneyTravelToTheDesk(throwPosition, throwTarget.position, throwTarget.eulerAngles, throwRandomAngle, throwDuration, throwDeskJumpPower, IsStackFul(), usedMoneyList);
            thrownMoneyCounter++;
            yield return new WaitForSeconds(stackingFrequency);
        }
    }

    bool IsStackFul()
    {
        return (usedMoneyList.Count < moneyStackPositions.childCount) ? false : true;
    }

    Transform GetPoolObjectFromThePool(List<Transform> listToGet)
    {
        Transform tempPoolObject = listToGet[0];
        tempPoolObject.gameObject.SetActive(true);
        listToGet.RemoveAt(0);
        return tempPoolObject;
    }

    public IEnumerator CollectTheMoney()
    {
        if (usedMoneyList.Count > 0 && !moneyCollectIsBusy)
        {
            moneyCollectIsBusy = true;
            for (int i = 0; i < thrownMoneyCounter; i++)
            {
                int normalizedIndex = i % moneyStackPositions.childCount;
                ConvertMoneyToMoneyUI(normalizedIndex);
                yield return new WaitForSeconds(0f);
            }
            CleanTheTable();

            thrownMoneyCounter = 0;
            moneyCollectIsBusy = false;
        }

    }


    void CleanTheTable()
    {
        for (int i = 0; i < usedMoneyList.Count; i++)
        {
            usedMoneyList[i].GetComponent<MoneyActor>().PoolItself();
        }
    }

    void ConvertMoneyToMoneyUI(int moneyIndex)
    {

        Transform tempMoneyUI = pooledMoneyUIList[0];
        tempMoneyUI.gameObject.SetActive(true);
        Vector2 tempMoneyUIPosition = WorldToScreenConverter(moneyStackPositions.GetChild(moneyIndex));
        Vector3 tempMoneyUITargetPosition = CalculateTheMoneyUIImageAnchoredPosition();
        tempMoneyUI.GetComponent<MoneyUIActor>().MoneyTravelToMoneyUIOnCanvas(tempMoneyUIPosition, tempMoneyUITargetPosition, moneyUITravelDuration);

        pooledMoneyUIList.RemoveAt(0);
        usedMoneyUIList.Add(tempMoneyUI);

        if (usedMoneyList.Count > 0)
        {
            usedMoneyList[0].GetComponent<MoneyActor>().PoolItself();
        }
    }

    Vector3 CalculateTheMoneyUIImageAnchoredPosition()
    {
        RectTransform moneyUIImage = UIManager.instance.InGameMoneyImage.GetComponent<RectTransform>();
        return moneyUIImage.position;

        // float width = Screen.width;
        // float height = Screen.height;

        // float moneyUIXMiddlePosRate = moneyUIImage.anchorMin.x + ((moneyUIImage.anchorMax.x - moneyUIImage.anchorMin.x)/2) - 0.5f;
        // float moneyUIYMiddlePosRate = moneyUIImage.anchorMin.y + ((moneyUIImage.anchorMax.y - moneyUIImage.anchorMin.y)/2 )- 0.5f;
        // Vector2 moneyUIPos = new Vector3(width * moneyUIXMiddlePosRate, height * moneyUIYMiddlePosRate, 0f);
        // return moneyUIPos;
    }

    Vector2 WorldToScreenConverter(Transform target)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        float h = Screen.height;
        float w = Screen.width;
        float x = screenPos.x - (w / 2);
        float y = screenPos.y - (h / 2);
        float s = UIManager.instance.mainCanvas.scaleFactor;
        return new Vector2(x, y) / s;
    }

    public void PrepareTheDeskMoney()
    {
        int amountOfThrowMoney = thrownMoneyCounter < moneyStackPositions.childCount ? thrownMoneyCounter : moneyStackPositions.childCount;
        for (int i = 0; i < amountOfThrowMoney; i++)
        {
            Transform moneyToThrow = GetPoolObjectFromThePool(pooledMoneyList);
            usedMoneyList.Add(moneyToThrow);
            int stackIndex = (i < moneyStackPositions.childCount) ? i : moneyStackPositions.childCount - 1;
            Transform throwTarget = moneyStackPositions.GetChild(stackIndex);
            moneyToThrow.position = throwTarget.position;
            moneyToThrow.eulerAngles = throwTarget.eulerAngles;
            //moneyToThrow.GetComponent<MoneyActor>().MoneyTravelToTheDesk(throwPosition, throwTarget.position, throwTarget.eulerAngles, throwRandomAngle, throwDuration, throwDeskJumpPower, IsStackFul(), usedMoneyList);
        }
    }

    #region Button

    [Title("Add Money on the desk for test purposes")]
    [Button("Add Money", ButtonSizes.Large)]
    void ButtonAddMoney()
    {
        StartCoroutine(ThrowMoney(3, testThrowPosition.position, 0.05f));
    }
    #endregion
}
