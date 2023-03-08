using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory20 : MonoBehaviour
{ 
    public int WaterAmount = 1;
    public int SeedAmount = 0;

    public bool TreeCanBePlaced = true;
    public bool TurretUpgradeCanBe = true;

    public float _Time = 10;
    private float maxTime = 10;
    private float newMaxTime = 10;

    public Slider _TimeRemaining;

    public int numberOfTrees = 0;

    public void SetWaterTime(float waterTime)
    {
        newMaxTime = waterTime;
    }
   
    public bool PlaceTree (int cost)
    {
       
        if(WaterAmount - cost >= 0)
        {
            //WaterAmount -= cost;
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

    public void PayWater (int cost)
    {
        WaterAmount -= cost;
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
            _TimeRemaining.value = maxTime - _Time;
        }
        else
        {
            maxTime = newMaxTime;

            _TimeRemaining.maxValue = maxTime;
            _Time = maxTime;
            WaterAmount++;
        }
    }
}
