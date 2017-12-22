using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWoman : MonoBehaviour, IInteractiveObject
{
    public event OnInteractionHandler InteractionStarted;
    public static bool m_oldWomanSpoken = false;
    public void StartInteraction()
    {
        m_oldWomanSpoken = true;
    }
}
