
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private ItemInteraction currentItem;
    private LockInteraction currentLock;
    private DoorInteraction currentDoor;

    public void SetCurrentItem(ItemInteraction item)
    {
        currentItem = item;
        currentLock = null;
        currentDoor = null;
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
        currentDoor = null;
    }

    public void ClearCurrentLock(LockInteraction lockObject)
    {
        if (currentLock == lockObject)
        {
            currentLock = null;
        }
    }

    public void SetCurrentDoor(DoorInteraction door)
    {
        currentDoor = door;
        currentItem = null;
        currentLock = null;
    }

    public void ClearCurrentDoor(DoorInteraction door)
    {
        if (currentDoor == door)
        {
            currentDoor = null;
        }
    }

    public void TriggerInteraction()
    {
        if (currentLock != null)
        {
            currentLock.TryInteract();
        }
        else if (currentDoor != null)
        {
            currentDoor.TryInteract();
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
