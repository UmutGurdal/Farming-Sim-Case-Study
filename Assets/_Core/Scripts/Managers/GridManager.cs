using UnityEngine;
using Muchwood.Utils;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Grid GridPrefab;
    [Space]
    [SerializeField] private Vector2Int GridSize;
    [SerializeField] private float Spacing;

#if UNITY_EDITOR
    [Button]
    private void BuildGrid()
    {
        RemoveGrid();

        transform.position = new Vector3((GridSize.x + ((GridSize.x - 1) * Spacing))/2,0, (GridSize.y + ((GridSize.y - 1) * Spacing)) / 2);

        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                Vector3 pos = new Vector3(i + (Spacing * i), 0, j + (Spacing * j));

                var grid = (Grid)PrefabUtility.InstantiatePrefab(GridPrefab);
                grid.transform.SetParent(transform);
                grid.transform.SetPositionAndRotation(pos, Quaternion.identity);
            }
        }

        transform.position = Vector3.zero;
    }

    [Button]
    private void RemoveGrid()
    {
        var grids = GetComponentsInChildren<Grid>();
        for (int i = grids.Length - 1; i >= 0; i--)
        {
            DestroyImmediate(grids[i].gameObject);
        }
    }
#endif
}
