
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private ItemInteraction currentItem;
    private LockInteraction currentLock;

    public void SetCurrentItem(ItemInteraction item)
    {
        currentItem = item;
        currentLock = null;
    }

    public void ClearCurrentItem(ItemInteraction item)
    {
        if (currentItem == item)
        {
            currentItem = null;
        }
    }

    public void SetCurrentLock(LockInteraction lockObject)
    {
        currentLock = lockObject;
        currentItem = null;
    }

    public void ClearCurrentLock(LockInteraction lockObject)
    {
        if (currentLock == lockObject)
        {
            currentLock = null;
        }
    }

    public void TriggerInteraction()
    {
        if (currentLock != null)
        {
            currentLock.TryInteract();
        }
        else if (currentItem != null)
        {
            currentItem.TryInteract();
        }
        else
        {
            Debug.Log("No hay ningún objeto interactuable cerca.");
        }
    }
}
