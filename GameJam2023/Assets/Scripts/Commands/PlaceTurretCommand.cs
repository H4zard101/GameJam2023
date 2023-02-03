using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTurretCommand : CommandManager.ICommand
{
    private Vector3Int m_From;
    private Vector3Int m_locationToPlace;
     public PlaceTurretCommand(Vector3Int location) 
    {
        m_locationToPlace = location;
      
    }
    
    public void Execute()
    {
        var unit = Gameboard.Instance.GetUnit(m_From);
        if (unit != null)
        {
            Gameboard.Instance.PlaceUnit(unit, m_locationToPlace);
           // Gameboard.Instance.SwitchTeam();
        }
    }

    public void Undo()
    {
        /*
        var unit = Gameboard.Instance.GetUnit(m_To);
        if (unit != null)
        {
            Gameboard.Instance.MoveUnit(unit, m_From);
        }

        Gameboard.Instance.SwitchTeam();
        */
    } 
}
