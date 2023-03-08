using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeUnit : Unit
{
    

    public int Distance;
    public GameObject[] spawnableTurrets;

    public float GetHealth_TreeSource()
    {
        if(gameObject.GetComponent<TreeSource>() != null)
        {
            return gameObject.GetComponent<TreeSource>().Health;
        }
        else
        {
            return 0f;
        }     
    }

    public float GetLevel_TreeSource()
    {
        if(gameObject.GetComponent<TreeSource>() != null)
        {
            return gameObject.GetComponent<TreeSource>().treeSourceLevel;
        }
        else
        {
            return 0f;
        }
    }

    public string GetTurretType_TreeSource()
    {
        if(gameObject.GetComponent<TreeSource>() != null)
        {
            return gameObject.GetComponent<TreeSource>().typeName;
        }
        else
        {
            return "null";
        }
    }
    
    public override int GetMoveCells(Vector3Int[] result, Gameboard board)
    {
        int count = 0;
        
        for (int y = -Distance; y <= Distance; ++y)
        {
            for (int x = -Distance; x <= Distance; ++x)
            {
                if(x == 0 && y == 0)
                    continue;
                
                if(Mathf.Abs(x) + Mathf.Abs(y) > Distance)
                    continue;
                
                var idx = m_CurrentCell + new Vector3Int(x,0, y);
                if (board.IsOnBoard(idx))
                {
                    result[count] = idx;
                    count++;
                }
            }
        }

        return count;
    }
}
