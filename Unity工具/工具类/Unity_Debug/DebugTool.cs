using UnityEngine;

public class DebugTool :Singleton<DebugTool> 
{
    public void Log(string debugstr)
    {
        if (Application.isEditor)
        {
            Debug.Log(debugstr);
        }
        else
        {
            Debug.LogError(debugstr);
        }
    }

    public void LogError(string  debugstr)
    {
        Debug.LogError(debugstr);
    }
	 
}
