using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomSpawner : MonoBehaviour
{
    // atom prefabs
    [SerializeField] GameObject hydrogenPrefab;
    [SerializeField] GameObject heliumPrefab;
    [SerializeField] GameObject carbonPrefab;
    [SerializeField] GameObject nitrogenPrefab;
    [SerializeField] GameObject oxygenPrefab;

    [SerializeField] float minSpawnTime = 1f; // min. time between spawns
    [SerializeField] float maxSpawnTime = 5f; // max. time between spawns
    [SerializeField] float totalAtoms = 0; // current total atoms in area
    [SerializeField] float maxAtoms = 200; // max allowed atoms in area
    [SerializeField] float randomNumber; // random number to determine which atom to spawn

    void Start()
    {
        // start spawn objects coroutine
        StartCoroutine(SpawnAtomAtRandomIntervals());
    }

    IEnumerator SpawnAtomAtRandomIntervals()
    {
        while (totalAtoms < maxAtoms)
        {
            // generate a random spawn position for atom
            Vector2 randomSpawnPosition = new Vector2(Random.Range(-88, 88), Random.Range(7, 160));

            // spawn random atom at random position
            randomNumber = Random.Range(1, 6);
            if (randomNumber == 1) // spawn hydrogen atom
            {
                GameObject hydrogenClone = Instantiate(hydrogenPrefab, randomSpawnPosition, Quaternion.identity);
                hydrogenClone.name = hydrogenPrefab.name;
                hydrogenClone.GetComponent<AtomBonding>().atomBonded = false;
            }
            else if (randomNumber == 2) // spawn helium atom
            {
                GameObject heliumClone = Instantiate(heliumPrefab, randomSpawnPosition, Quaternion.identity);
                heliumClone.name = heliumPrefab.name;
                heliumClone.GetComponent<AtomBonding>().atomBonded = false;
            }
            else if (randomNumber == 3)  // spawn carbon atom
            {
                GameObject carbonClone = Instantiate(carbonPrefab, randomSpawnPosition, Quaternion.identity);
                carbonClone.name = carbonPrefab.name;
                carbonClone.GetComponent<AtomBonding>().atomBonded = false;
            }
            else if (randomNumber == 4)  // spawn nitrogen atom
            {
                GameObject nitrogenClone = Instantiate(nitrogenPrefab, randomSpawnPosition, Quaternion.identity);
                nitrogenClone.name = nitrogenPrefab.name;
                nitrogenClone.GetComponent<AtomBonding>().atomBonded = false;
            }
            else if (randomNumber == 5)  // spawn oxygen atom
            {
                GameObject oxygenClone = Instantiate(oxygenPrefab, randomSpawnPosition, Quaternion.identity);
                oxygenClone.name = oxygenPrefab.name;
                oxygenClone.GetComponent<AtomBonding>().atomBonded = false;
            }

            // increase number of total number of atoms
            totalAtoms += 1;

            // wait for a random amount of time before spawning an atom again
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}