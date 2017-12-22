using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triforce : MonoBehaviour, IInteractiveObject
{
    public event OnInteractionHandler InteractionStarted;
    public static bool m_zeldaSpoken = false;
    public static bool m_triforceTaken = false;

    public void StartInteraction()
    {
        m_triforceTaken = true;
    }
}
