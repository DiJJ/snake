using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Snake _snake;
    [SerializeField] private Button _button;

    private void Awake()
    {
        _snake.onDie += () => _button.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("DevScene");
    }
}
