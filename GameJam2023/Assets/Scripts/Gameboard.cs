using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameboard : MonoBehaviour
{
    public static Gameboard Instance => s_Instance;
    private static Gameboard s_Instance;
    
    public int Width;
    public int Height;

    private int turn_num = 1;
    private int timer = 0;
    private float subtimer = 0;

    public GameObject[] all_spawners;

    public Grid Grid => m_Grid;
    public AnimationSystem AnimationSystem => m_AnimSystem;
    
    
    private Unit[,] m_Content;

    private Grid m_Grid;
    private AnimationSystem m_AnimSystem;

    private Plane m_Plane;

    public Inventory20 inventory;

    [SerializeField] private ResourceUI resourceUI;
    [SerializeField] private TurnUI turnUI;

    public GameObject MotherTree;

    public int cost = 5;
    
    public int treesPlanted = 0;

    // Start is called before the first frame update
    void Awake()
    {
        s_Instance = this;
        m_Grid = GetComponent<Grid>();
        m_Content = new Unit[Width,Height];
        m_AnimSystem = new AnimationSystem();
        
        m_Plane = new Plane(Vector3.up, Vector3.zero);
        inventory = FindObjectOfType<Inventory20>();
        resourceUI.SetInventory(inventory);
    }

    private void Start()
    {
        UpdateTurnIndicator();
        all_spawners[0].SetActive(true);
    }

    private void Update()
    {
        m_AnimSystem.Update();

        subtimer += Time.deltaTime;

        if (subtimer > 1)
        {
            timer += 1;
            subtimer = 0;
        }

        if (timer % 30 == 0 && timer > 0)
        {
            turn_num += 1;
            UpdateTurnIndicator();
            timer += 1;

            if (turn_num == 13)
            {
                foreach(GameObject spawner in all_spawners)
                {
                    spawner.GetComponent<AISpawner>().spawnInterval = 0.5f;
                }
            }
            else
            {
                all_spawners[turn_num - 1].SetActive(true);
            }
        }
    }

    public void SetUnit(Vector3Int cell, Unit unit)
    {
        if(!IsOnBoard(cell))
            return;
        
        m_Content[cell.x, cell.z] = unit;
    }

    public Unit GetUnit(Vector3Int cell)
    {
        if (!IsOnBoard(cell))
            return null;

        return m_Content[cell.x, cell.z];
    }

    public Vector3Int GetClosestCell(Vector3 pos)
    {
        var idx = m_Grid.WorldToCell(pos);

        if (idx.x == 0) idx.x = 0;
        else if (idx.x >= Width) idx.x = Width - 1;
        if (idx.z == 0) idx.z = 0;
        else if (idx.z >= Height) idx.z = Height - 1;

        return idx;
    }

    public bool IsOnBoard(Vector3Int cell)
    {
        return cell.x >= 0 && cell.x < Width && cell.z >= 0 && cell.z < Height;
    }

    public bool Raycast(Ray ray, out Vector3Int cell)
    {
        cell = Vector3Int.zero;
        
        //First raycast against collider to check if we clicked on any unit directly
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Unit u = hit.collider.GetComponentInParent<Unit>();
            if (u != null)
            {
                cell = u.CurrentCell;
                return true;
            }
        }
        
        if (m_Plane.Raycast(ray, out float d))
        {
            var clickedCell = Gameboard.Instance.Grid.WorldToCell(ray.GetPoint(d));

            if (IsOnBoard(clickedCell))
            {
                cell = clickedCell;
                return true;
            }

            return false;
        }

        return false;
    }

    public void MoveUnit(Unit u, Vector3Int to, bool animate = true)
    {
        //unit that aren't on the board have (-1,-1-,1) as their current cell
        if(u.CurrentCell.x != -1)   
            m_Content[u.CurrentCell.x, u.CurrentCell.z] = null;
        
        m_Content[to.x, to.z] = u;
        u.CurrentCell = to;

        if (animate)
        {
            m_AnimSystem.NewAnim(
                u.transform.transform, 
                m_Grid.GetCellCenterWorld(to),
                3.0f);
        }
    }

    public GameObject TreePlacementUI;
    private Unit lastUnit;
    private Vector3Int lastLocationToPlace;

    public void PlaceUnit(Unit U, Vector3Int locationToPlace)
    {
        if(inventory.PlaceTree(cost))
        {
            lastUnit = U;
            lastLocationToPlace = locationToPlace;
            TreePlacementUI.SetActive(true);
        }

    }

    public void SelectedTreeAndTurret(string type)
    {
        Debug.LogWarning("Turret Type : " + type);

        var tree = Instantiate(lastUnit.turrets[0], lastLocationToPlace, Quaternion.identity);
        tree.GetComponent<TreeSource>().SetTurret(type);
        TreePlacementUI.SetActive(false);

        TreeManager.instance.allTrees.Add(tree);
        tree.tag = "Tree";
        AudioPlayback.PlayOneShot(AudioManager.Instance.references.turretPlacedEvent, null);
        treesPlanted += 1;
        //AudioManager.Instance.parameters.SetParamByName(AudioManager.Instance.musicInstance, "TreeCount", treesPlanted);

        tree.gameObject.GetComponent<TreeRoots>().start = MotherTree;
        tree.gameObject.GetComponent<TreeRoots>().end = tree;
        tree.gameObject.GetComponent<TreeRoots>().Grow();
        Debug.Log("Tree count" + treesPlanted);
    }



    void UpdateTurnIndicator()
    {
        //TurnIndicatorText.text = (m_CurrentTeam == Unit.Team.White ? "White" : "Black") + " is playing";
        turnUI.UpdateTurn(turn_num);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        
        for (int x = 0; x < Width; ++x)
        {
            Gizmos.DrawLine(Vector3.right * x, Vector3.right * x + Height * Vector3.forward);
        }
        
        Gizmos.DrawLine(Vector3.right * Width, Vector3.right * Width + Height * Vector3.forward);
        
        for (int y = 0; y < Height; ++y)
        {
            Gizmos.DrawLine(Vector3.forward * y, Vector3.forward * y + Vector3.right * Width);
        }
        
        Gizmos.DrawLine(Vector3.forward * Height, Vector3.forward * Height + Vector3.right * Width);
    }
}
