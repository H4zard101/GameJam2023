using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    public TMP_Text waterAmount;
    public TMP_Text seedAmount;
    public Inventory20 inventory;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetInventory(Inventory20 inventory)
    {
        this.inventory = inventory;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null)
        {
            waterAmount.text = inventory.WaterAmount.ToString();
            seedAmount.text = inventory.SeedAmount.ToString();
        }
    }
}
