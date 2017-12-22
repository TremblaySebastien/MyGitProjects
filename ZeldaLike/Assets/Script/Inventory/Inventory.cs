using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public UnityEngine.UI.Text keyCountText;
    public UnityEngine.UI.Text rupeeCountText;
    public UnityEngine.UI.Text bombCountText;
    public InventorySection<Key> keys = new InventorySection<Key>();
    public InventorySection<Rupee> rupees = new InventorySection<Rupee>();
    public InventorySection<Bomb> bombs = new InventorySection<Bomb>();
    public static int bombQty;

    public void AddKey(Key key)
    {
        keys.AddItem(key);
        keyCountText.text = "X"+keys.GetItems().Count();
    }

    public void AddRupee(Rupee rupee)
    {
        rupees.AddItem(rupee);
        rupeeCountText.text = "X" + rupees.GetItems().Count();
    }

    public void AddBomb(Bomb bomb)
    {
        for(int i = 0; i < 20; i++)
        {
            bombs.AddItem(bomb);
        }
        
        bombCountText.text = "X" + bombs.GetItems().Count();
        bombQty = bombs.GetItems().Count();
    }

    public void RemoveBomb(Bomb bomb)
    {
        bombs.RemoveItem(bomb);
        bombCountText.text = "X" + bombs.GetItems().Count();
    }
}
