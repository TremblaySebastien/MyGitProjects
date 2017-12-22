using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foe : MonoBehaviour, IInteractiveObject
{
    public event OnInteractionHandler InteractionStarted;
    public static bool m_FoeActivated = false;

    public void StartInteraction()
    {
        
    }

}
