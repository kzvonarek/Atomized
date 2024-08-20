using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterBear : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float bearSpeed; // speed Waterbear moves at
    [SerializeField] float bearGrowthRate;
    [SerializeField] Transform playerPos; // get position of player
    [SerializeField] GameObject playerObj;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationModifier;
    [SerializeField] GameObject atomSpawner;
    private AtomSpawner aSscript;
    [SerializeField] GameObject gameManager;
    private GameManagement gMscript;
    private PlayerBonding pBscript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pBscript = playerObj.GetComponent<PlayerBonding>();
        aSscript = atomSpawner.GetComponent<AtomSpawner>();
        gMscript = gameManager.GetComponent<GameManagement>();
    }

    void Update()
    {
        // water bear constantly follows player at a certain speed
        Vector2 direction = (playerPos.position - transform.position).normalized;
        Vector2 newPosition = rb.position + direction * bearSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);

        // rotation that makes Waterbear sprite head always face player
        // credit to: https://www.youtube.com/watch?v=RNPetUa8_PQ
        Vector3 vectorToTarget = playerObj.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

        // Waterbear scales in size over time
        if (this.gameObject.transform.localScale.x < 200 && this.gameObject.transform.localScale.y < 200 && this.gameObject.transform.localScale.z < 200)
        {
            this.transform.localScale += new Vector3(bearGrowthRate, bearGrowthRate, bearGrowthRate) * Time.deltaTime;
        }

        // Waterbear increases speed over time, once it fully grew
        if (this.gameObject.transform.localScale.x >= 200 && this.gameObject.transform.localScale.y >= 200 && this.gameObject.transform.localScale.z >= 200)
        {
            bearSpeed += Time.deltaTime / 20;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Water Bear 'kills' player and ends game
            gMscript.dead = true;
        }

        // Water Bear destroys atoms in its way, reduces total atom count
        if (other.gameObject.CompareTag("Atom"))
        {
            Destroy(other.gameObject);
            aSscript.totalAtoms -= 1;

            // check if atom was bonded to Player, and remove it from list/decrease count from total
            AtomBonding aBscript = other.gameObject.GetComponent<AtomBonding>();
            if (aBscript.atomBonded)
            {
                pBscript.currAtoms.Remove(other.gameObject);

                if (other.gameObject.name == "Hydrogen Atom")
                {
                    pBscript.totalHydrogen -= 1;
                    pBscript.bondFunction(-2, -0.3f); // decrease FOV by 2, increase speed by 0.3f
                }

                if (other.gameObject.name == "Helium Atom")
                {
                    pBscript.totalHelium -= 1;
                    pBscript.bondFunction(-1, -0.1f); // decrease FOV by 1, increase speed by 0.1f
                }

                if (other.gameObject.name == "Carbon Atom")
                {
                    pBscript.totalCarbon -= 1;
                    pBscript.bondFunction(-5, -1f); // decrease FOV by 5, increase speed by 1f
                }

                if (other.gameObject.name == "Nitrogen Atom")
                {
                    pBscript.totalNitrogen -= 1;
                    pBscript.bondFunction(-4, -0.7f); // decrease FOV by 4, increase speed by 0.7f
                }

                if (other.gameObject.name == "Oxygen Atom")
                {
                    pBscript.totalOxygen -= 1;
                    pBscript.bondFunction(-3, -0.5f); // decrease FOV by 3, increase speed by 0.5f
                }
            }
        }
    }
}
