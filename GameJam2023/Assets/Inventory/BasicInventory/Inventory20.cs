using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory20 : MonoBehaviour
{ 
    public int WaterAmount = 10;
    public int SeedAmount = 10;

    public bool TreeCanBePlaced = true;
    public bool TurretUpgradeCanBe = true;

    public float _Time = 10;
    public float maxTime = 10;

    public int numberOfTrees = 0;

    public bool PlaceTree (int cost)
    {
       
        if(WaterAmount - cost >= 0)
        {
            WaterAmount -= cost;
            TreeCanBePlaced = true;
            numberOfTrees++;
            AudioManager.Instance.parameters.SetParamByName(AudioManager.Instance.musicInstance, "TreeCount", numberOfTrees);
            Debug.Log(WaterAmount.ToString());
        }
        else
        {
            TreeCanBePlaced = false;

        }
        return TreeCanBePlaced;
    }

    public bool UpgradeTurret(int cost)
    {
        if(SeedAmount - cost >= 0)
        {
            SeedAmount -= cost;
            TurretUpgradeCanBe = true;
        }
        else
        {
            TurretUpgradeCanBe = false;
        }
        return TurretUpgradeCanBe;
    }

    public void Update()
    {


        if(_Time > 0)
        {
            _Time -= Time.deltaTime;

        }

        else
        {
            _Time = maxTime;
            WaterAmount++;
        }

    }
}
