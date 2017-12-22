using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldMan : MonoBehaviour, IInteractiveObject
{
    public event OnInteractionHandler InteractionStarted;
    public static bool m_oldManSpoken = false;

    public void StartInteraction()
    {
        m_oldManSpoken = true;
    }
}
