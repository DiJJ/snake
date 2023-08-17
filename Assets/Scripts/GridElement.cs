using UnityEngine;

public class GridElement : MonoBehaviour
{
    protected Vector3Int _gridPosition;

    public virtual Vector3Int GridPosition
    {
        get => _gridPosition;
        
        set
        {
            _gridPosition = value;
            GridPlacer.Instance.PlaceOnGrid(transform, value);
        }
    }

    private void Start()
    {
        //Snaps object to nearest cell
        GridPosition = GridPlacer.Instance.PositionToCell(transform.position);
        
        //Weird, but collision doesn't work without this
        var position = transform.position;
        position = new Vector3(position.x, position.y, 0);
        transform.position = position;
    }
}
