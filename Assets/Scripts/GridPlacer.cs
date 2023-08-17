using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridPlacer : MonoBehaviour
{
    private static GridPlacer _instance;

    public static GridPlacer Instance => _instance;

    private Grid _grid;
    
    private void Awake()
    {
        _grid = GetComponent<Grid>();
        _instance = this;
    }

    public void PlaceOnGrid(Transform item, Vector3Int position)
    {
        item.position = _grid.GetCellCenterWorld(position);
    }

    public Vector3 CellToPosition(Vector3Int position) => _grid.GetCellCenterWorld(position);

    public Vector3Int PositionToCell(Vector3 position) => _grid.WorldToCell(position);
}
