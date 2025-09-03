using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public int money = 10;
    public bool isWaveGoing;
    public Material greenMat, redMat;
    private void Awake()
    {
        Instance = this;
    }
}
