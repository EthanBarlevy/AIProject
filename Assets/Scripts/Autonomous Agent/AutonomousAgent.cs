using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    
    void Update()
    {
        var gamObjects = perception.GetGameObjects();
        foreach (var gamObject in gamObjects) 
        {
            Debug.DrawLine(transform.position, gamObject.transform.position);
        }
    }
}
