using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UpgradeWindowActor : SerializedMonoBehaviour
{
    public UpgradeWindowUpgradeOfficer upgradeWindowUpgradeOfficer;
    public UpgradeButtonsUpdateOfficer upgradeButtonsUpdateOfficer;
    public Dictionary<string, UpgradeButtonActor> upgradeButtons = new Dictionary<string, UpgradeButtonActor>();

    public List<UpgradeTabActor> upgradeTabList = new List<UpgradeTabActor>();

    public void HandleTabSwitchs(int tabIndex)
    {
        foreach (UpgradeTabActor tab in upgradeTabList)
        {
            tab.ActivateOrDeactivateTheTab(false);
        }

        upgradeTabList[tabIndex].ActivateOrDeactivateTheTab(true);
    }

    public void GetPrepared()
    {
        upgradeButtonsUpdateOfficer.UpdateTheButtons();
    }
}
