using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void OnInteractionHandler(IInteractiveObject source);

public interface IInteractiveObject
{
    void StartInteraction();
    event OnInteractionHandler InteractionStarted;
}
