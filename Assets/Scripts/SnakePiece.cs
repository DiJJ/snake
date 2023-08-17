using UnityEngine;

public class SnakePiece : GridElement
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public SpriteRenderer Renderer => _spriteRenderer;
    
    private Vector3Int _followPosition;
    public Vector3Int FollowPosition => _followPosition;
    
    public override Vector3Int GridPosition
    {
        get => _gridPosition;
        
        set
        {
            _followPosition = _gridPosition;
            _gridPosition = value;
            GridPlacer.Instance.PlaceOnGrid(transform, value);
        }
    }
}
