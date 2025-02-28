using Muchwood.Utils;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [field: SerializeField, ReadOnly] public Vector2Int Coordinate { get; private set; }
    public bool isOccupied;

    public void SetCoordinate(Vector2Int coordinate)
    {
        Coordinate = coordinate;
    }
}
