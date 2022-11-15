public class LevelManager : Manager
{
    public static LevelManager instance;
    public LevelCreateOfficer levelCreateOfficer;
    public LevelPowerUpOfficer levelPowerUpOfficer;

    private void Awake()
    {
        StaticCheck();
    }
    
    void StaticCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public override void GameStartProcess()
    {
        levelCreateOfficer.CreateLevelProcess();
    }
    public override void PreLevelInstantiateProcess()
    {
        levelCreateOfficer.currentLevel.GetComponent<LevelActor>().PreLevelInstantiateProcess();
    }

}
