using UnityEngine;
using Muchwood.Utils;
using Grid = Muchwood.Grid;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static event Action OnConstructed;

    [SerializeField] private Grid GridPrefab;
    [Space]
    [SerializeField] private Vector2Int GridSize;
    [SerializeField] private float Spacing;

    public List<Grid> AllGrids;    

#if UNITY_EDITOR
    [Button]
    private void BuildGrid()
    {
        RemoveGrid();

        GameManager.ins.Camera.transform.position = new Vector3(GridSize.x * (1+Spacing)/2f, 40, -12);

        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                Vector3 pos = new Vector3(i + (Spacing * i), 0, j + (Spacing * j));

                var grid = (Grid)PrefabUtility.InstantiatePrefab(GridPrefab);
                grid.transform.SetPositionAndRotation(pos, Quaternion.identity);
                grid.transform.SetParent(transform);
                grid.SetCoordinate(new Vector2Int(i, j));

                AllGrids.Add(grid);
            }
        }
    }

    [Button]
    private void RemoveGrid()
    {
        AllGrids = new List<Grid>();
        var grids = GetComponentsInChildren<Grid>();
        for (int i = grids.Length - 1; i >= 0; i--)
        {
            DestroyImmediate(grids[i].gameObject);
        }
    }
#endif

    public bool CanBuild(Vector2Int targetCoordinate, Vector2Int buildingSize) 
    {
        if(targetCoordinate.y + buildingSize.y > GridSize.y)
            return false;

        if(targetCoordinate.x + buildingSize.x > GridSize.x)
            return false;

        for(int i = 0; i < buildingSize.x; i++)
        {
            for(int j = 0; j < buildingSize.y; j++)
            {
                if (AllGrids[GetIndex(targetCoordinate + new Vector2Int(i,j))].isOccupied)
                    return false;
            }
        }

        return true;
    }

    public void Constructed(Vector2Int targetCoordinate, Vector2Int buildingSize) 
    {
        for (int i = 0; i < buildingSize.x; i++)
        {
            for (int j = 0; j < buildingSize.y; j++)
            {
                AllGrids[GetIndex(targetCoordinate + new Vector2Int(i, j))].isOccupied = true;
            }
        }

        OnConstructed?.Invoke();
    }

    public void Destruct(Vector2Int targetCoordinate, Vector2Int buildingSize) 
    {
        for (int i = 0; i < buildingSize.x; i++)
        {
            for (int j = 0; j < buildingSize.y; j++)
            {
                AllGrids[GetIndex(targetCoordinate + new Vector2Int(i, j))].isOccupied = false;
            }
        }
    }

    public int GetIndex(Vector2Int coordinate)
    {
        return GridSize.y * coordinate.x + coordinate.y;
    }
}
