using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariablesData : MonoBehaviour
{
    public List<int> PlayerUpgradeButtonCosts = new List<int>();
    public List<float> PlayerUpgradeSpeedValues = new List<float>();
    public List<int> PlayerUpgradeCapacityValues = new List<int>();

    public List<int> WorkerUpgradeButtonCosts = new List<int>();
    public List<float> WorkerUpgradeSpeedValues = new List<float>();

    public List<int> TruckUpgradeButtonCosts = new List<int>();
    public List<int> TruckUpgradeCapacityValues = new List<int>();
    public List<float> TruckUpgradeSpeedValues = new List<float>();

    public List<int> EnvironmentButtonCosts = new List<int>();
}
