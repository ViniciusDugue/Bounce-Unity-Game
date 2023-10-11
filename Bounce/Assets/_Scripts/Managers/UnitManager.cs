using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Unit _BasicEnemy_1;
    [SerializeField] private Unit _BasicEnemy_2;
    [SerializeField] private Unit _Tower_Enemy;

    private Unit _spawnedUnit;
    // makes list to store units for public reading and local writing in this manager
    public List<Unit> Units {get; private set;  } = new List<Unit>();
    private List<Vector3> towerSpawnLocations = new List<Vector3>
    {
        new Vector3(-37.3100014f,-34.4799995f,-0.306776494f),
        new Vector3(0.200000003f,-16.8299999f,-0.306776494f),
        new Vector3(-33.9900017f,-17.1399994f,-0.306776494f),
        new Vector3(-54.5699997f,-16.8199997f,-0.306776494f),
    };

    private List<Vector3> enemy_1SpawnLocations = new List<Vector3>
    {
        new Vector3(3.29999995f,-14.1000004f,-0.306776494f),
        new Vector3(3.29999995f,-15.1000004f,-0.306776494f)
    };

    public GameObject unitManager;
    void OnEnable()
    {
        unitManager.SetActive(false);
    }

    public void ActivateUnitManager()
    {
        unitManager.SetActive(true);

    }

    public void DeactivateUnitManager()
    {
        unitManager.SetActive(false);
    }

    public void SpawnEnemies_Level_1()
    {
        // adds unitPrefab unit to Units list and instantiates it 
        Debug.Log("Spawning Enemies");
        foreach (Vector3 SpawnLocation in towerSpawnLocations)
        {
            SpawnEnemy(_Tower_Enemy,SpawnLocation );
        }

        foreach (Vector3 SpawnLocation in enemy_1SpawnLocations)
        {
            SpawnEnemy(_BasicEnemy_2,SpawnLocation);
        }

        // for(int i=0; i<2; i++)
        // {
        //     SpawnEnemy(_Tower_Enemy,new Vector3(8,i*20-10,0));
        // }
        // for(int i=0; i<2; i++)
        // {
        //     SpawnEnemy(_BasicEnemy_1,new Vector3(12,i*5,0));
        // }
        // for(int i=0; i<2; i++)
        // {
        //     SpawnEnemy(_BasicEnemy_2,new Vector3(-12,i*5,0));
        // }
    }
    private void Update()
    {
        
        // print(Units[0].transform.position);
    }
    //spawns in enemy at position and adds to Units list
    public void SpawnEnemy(Unit enemyPrefab, Vector3 position)
    {
        _spawnedUnit = Instantiate(enemyPrefab, position, Quaternion.identity);
        Units.Add(_spawnedUnit);
    }
}
