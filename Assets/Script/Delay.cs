using UnityEngine;

public class Delay
{
    public float WaitTime;
    private float _completionTime = float.MaxValue;
 
    public Delay(float waitTime, bool stopped = false)
    {
        WaitTime = waitTime;
        
        if (!stopped)
            Reset();
    }
    
    public float RemainingTime()
    {
        return _completionTime - Time.time;
    }
 
    public void Reset()
    {
        _completionTime = Time.time + WaitTime;
    }
    
    public void Stop()
    {
        _completionTime = float.MaxValue;
    }
 
    public bool IsReady => Time.time >= _completionTime;
}
