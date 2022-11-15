using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelDataOfficer : SerializedMonoBehaviour
{
    [SerializeField] LevelActor levelActor;
    public Dictionary<int, GameObject> activeRooms = new Dictionary<int, GameObject>();
    public List<ActivisionPointAnchor> levelActivisionPoints = new List<ActivisionPointAnchor>();
    [SerializeField] List<int> investmentLeftAmountsTest = new List<int>();

    public void AssignLevelDatas()
    {
        foreach (int roomIndex in activeRooms.Keys)
        {
            if (!levelActor.levelRoomOfficer.levelRooms[roomIndex].activeSelf)
            {
                levelActor.levelRoomOfficer.ActivateARooom(roomIndex);
                //levelActor.levelRoomOfficer.levelRooms[roomIndex].SetActive(true);
            }
            activeRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.AssignTheVariables();
        }
        SoundManager.instance.MusicOnOff(UIManager.instance.settingsMenuActor.musicState);
    }

    public void LetMeKnowRoomIsActivated(int roomIndex, GameObject roomGameObject)
    {
        activeRooms.Add(roomIndex, roomGameObject);
    }

    public List<int> investmentLeftAmountsForActivisionPoints() 
    {
        List<int> investmentLeftAmounts = new List<int>();
        foreach (ActivisionPointAnchor activisionPoint in levelActivisionPoints)
        {
            investmentLeftAmounts.Add(activisionPoint.ActivisionCalculateOfficer.totalInvestmentRequired);
        }
        return investmentLeftAmounts;
    }

    public void AssignLeftInvestmentAmounts(List<int> investmentLeftAmounts) 
    {
        investmentLeftAmountsTest = investmentLeftAmounts;
        if (investmentLeftAmounts.Count > 0)
        {
            for (int i = 0; i < levelActivisionPoints.Count; i++)
            {
                levelActivisionPoints[i].ActivisionCalculateOfficer.totalInvestmentRequired = investmentLeftAmounts[i];
                levelActivisionPoints[i].ActivisionCalculateOfficer.VisualProcess(investmentLeftAmounts[i]);
            }
        }        
    }
}
