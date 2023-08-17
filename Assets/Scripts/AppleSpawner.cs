using UnityEngine;
using Random = UnityEngine.Random;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private Apple _apple;
    [SerializeField] private float _spawnRadius;
    private void Start()
    {
        SpawnApple();
    }

    private void SpawnApple()
    {
        var randPos = (Vector3)Random.insideUnitCircle * _spawnRadius;
        var apple = Instantiate(_apple, randPos, Quaternion.identity);
        apple.OnEaten += SpawnApple;
    }
}
