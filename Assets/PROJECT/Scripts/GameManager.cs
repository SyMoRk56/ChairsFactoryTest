using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
