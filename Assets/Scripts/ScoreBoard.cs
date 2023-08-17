using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Snake _snake;
    [SerializeField] private TextMeshProUGUI _text;

    private int _score;

    private void Awake()
    {
        _snake.onGrow += () => _text.text = $"Score: {++_score}";
        _snake.onDie += () => _text.text = $"Score: 0000";
    }
}
