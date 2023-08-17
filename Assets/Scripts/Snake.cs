using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    [SerializeField] private SnakePiece _snakePiece;
    [SerializeField, Tooltip("Grid cells per second")] private float _snakeSpeed;
    
    private LinkedList<SnakePiece> _snake;
    private WaitForSeconds _timeDelay;
    private Vector3Int _movementDirection;
    private Vector3Int _inputDirection;
    private LinkedListNode<SnakePiece> _head;

    public event Action onGrow;
    public event Action onTurn;
    public event Action onDie;

    private void Awake()
    {
        _snake = new();
        for (int i = 0; i < 3; i++)
        {
            var piece = Instantiate(_snakePiece, transform);
            _snake.AddLast(piece);
        }

        _head = _snake.First;
        _timeDelay = new WaitForSeconds(1f / _snakeSpeed);

        StartCoroutine(UpdateSnake());
    }
    
    private IEnumerator UpdateSnake()
    {
        while (true)
        {
            UpdateMovementDirection();
            MoveSnake();
            CheckHead(_head.Value.GridPosition);
            yield return _timeDelay;
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void UpdateMovementDirection()
    {
        if (_inputDirection == _movementDirection || _inputDirection == -_movementDirection)
            return;
        _movementDirection = _inputDirection;
        onTurn?.Invoke();
    }
    
    private void MoveSnake()
    {
        _head.Value.GridPosition += _movementDirection;
            
        var piece = _head;
        var alpha = 1f;
        while (piece != null)
        {
            piece.Value.Renderer.color = new Color(255, 255, 255, alpha);
            alpha -= 1f / (_snake.Count + 1);
            if (piece.Next != null)
            {
                piece.Next.Value.GridPosition = piece.Value.FollowPosition;
            }
            piece = piece.Next;
        }
    }
    
    private void CheckHead(Vector3Int position)
    {
        var checkPos =  GridPlacer.Instance.CellToPosition(position);
        var coll = Physics2D.OverlapBox(checkPos, Vector2.one * .5f, 0f);
        
        if (coll == null)
            return;
        
        if (coll.TryGetComponent(out GridElement element) == false)
            return;
        
        if (element is Apple apple)
        {
            apple.Eat();
            Grow();
        }
        
        if (element is Wall)
            Die();
        
        if (_snake.Count > 4 && element is SnakePiece && element != _head.Value)
            Die();
    }
    
    private void Grow()
    {
        var newPiece = Instantiate(_snakePiece, 
                                    position: _snake.Last.Value.GridPosition, 
                                    rotation: Quaternion.identity, 
                                    parent: transform);
        _snake.AddLast(newPiece);
        onGrow?.Invoke();
    }

    private void Die()
    {
        onDie?.Invoke();
        Destroy(gameObject);
    }
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed is not true)
            return;
        
        var input = context.ReadValue<Vector2>();
        _inputDirection = new Vector3Int((int)input.x, (int)input.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            var head = _snake.First;
            var newHeadPos = head.Value.GridPosition + _movementDirection;
            var checkPos =  GridPlacer.Instance.CellToPosition(newHeadPos);
            Gizmos.DrawWireCube(checkPos, Vector3.one);
        }
    }
}
