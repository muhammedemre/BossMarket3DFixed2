public class CameraManager : Manager
{
    public static CameraManager instance;
    public CameraActor cameraActor;

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
}
