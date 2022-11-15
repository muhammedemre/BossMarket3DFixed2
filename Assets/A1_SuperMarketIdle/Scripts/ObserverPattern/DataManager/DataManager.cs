using UnityEngine;

public class DataManager : Manager
{
    public static DataManager instance;
    public DataSaveAndLoadOfficer DataSaveAndLoadOfficer;
    public GameVariablesData gameVariablesData;
    public bool tutorialFinished = false;

    private void Awake()
    {
        StaticCheck();
        Application.quitting += Save;
    }
    
    void StaticCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public override void PreGameStartProcess()
    {
        DataSaveAndLoadOfficer.LoadTheData();
        GameManager.instance.gameManagerObserverOfficer.Publish(ObserverSubjects.GameStart);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            DataSaveAndLoadOfficer.SaveTheData();
        }
    }

    private void OnApplicationQuit()
    {
        //DataSaveAndLoadOfficer.SaveTheData();
    }

    static void Save()
    {
        DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();
    }
}
