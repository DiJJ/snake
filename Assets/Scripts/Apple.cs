using System;

public class Apple : GridElement
{
    public event Action OnEaten;

    public void Eat()
    {
        OnEaten?.Invoke();
        Destroy(gameObject);
    }
}
