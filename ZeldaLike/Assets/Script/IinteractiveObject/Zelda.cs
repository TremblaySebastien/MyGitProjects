using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zelda : MonoBehaviour, IInteractiveObject
{
    public event OnInteractionHandler InteractionStarted;
    public static bool m_zeldaSpoken = false;

    public void StartInteraction()
    {
        m_zeldaSpoken = true;
    }

}
