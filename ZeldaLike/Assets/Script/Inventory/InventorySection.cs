using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySection<T> where T : InventoryObject
{
    public List<T> itemList = new List<T>();

    public void AddItem(T item)
    {
        itemList.Add(item);
    }

    public void RemoveItem(T item)
    {
        itemList.Remove(item);
    }

    public T[] GetItems()
    {
        return itemList.ToArray();
    }
}
