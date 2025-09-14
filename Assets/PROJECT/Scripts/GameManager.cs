using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text moneyText;

    int m_money = 50;
    public int money { get { return m_money; } set { moneyText.text = "Δενεγ: " + value.ToString(); m_money = value; } }
    public bool isWaveGoing;
    public Material greenMat, redMat;
    private void Awake()
    {
        m_money = 50;
        Instance = this;
    }
    private void Start()
    {
        if((SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))) Destroy(gameObject);
        print("Start GM");
        Load();
        moneyText.text = moneyText.text = "Δενεγ: " + m_money.ToString();
        StartCoroutine(SaveCoroutine());
    }
    IEnumerator SaveCoroutine()
    {
        while (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            Save();
            yield return new WaitForSeconds(5);
        }
    }
    private void Update()
    {
        if (!isWaveGoing)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FindFirstObjectByType<WaveSpawner>().StartNextWave();
                FindFirstObjectByType<PlayerBuilder>().CancelBuilding();
            }
        }
        if (Input.GetKeyDown(KeyCode.I)) 
        { 
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(1);
        }
        
    }
    public void Save()
    {
        Building[] buildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
        BuildingScriptableObject[] sc = Resources.LoadAll<BuildingScriptableObject>("");
        var scl = sc.ToList();
        for (int i = 0; i < buildings.Length; i++)
        {
            Building building = buildings[i];
            PlayerPrefs.SetString(i.ToString(), building.transform.position.x + "|" + building.transform.position.z + "|"+ building.transform.eulerAngles.y+ "|" + scl.IndexOf(building.building) + "|" + building.upgrade);
            print("Save" + i);
        }
        print("Save");
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        print(1);
        money = PlayerPrefs.GetInt("Money", 50);
        BuildingScriptableObject[] sc = Resources.LoadAll<BuildingScriptableObject>("");
        for (int i = 0; i < 1000; i++)
        {
            if (!PlayerPrefs.HasKey(i.ToString())) continue;

            print("Load");
            string s = PlayerPrefs.GetString(i.ToString());
            string[] splitted = s.Split('|');
            string sPosX = splitted[0];
            string sPosZ = splitted[1];
            string sRotY = splitted[2];


            int soIndex = int.Parse(splitted[3]);
            int upgrade = int.Parse(splitted[4]);

            if (soIndex >= 0 && soIndex < sc.Length)
            {
                BuildingScriptableObject so = sc[soIndex];
                var b = Instantiate(so.prefab, new Vector3(float.Parse(sPosX), -1, float.Parse(sPosZ)), Quaternion.Euler(0, float.Parse(sRotY),0)).GetComponent<Building>();
                b.building = so;
                b.upgrade = upgrade;
                b.Setup();
            }
        }
    }
}
