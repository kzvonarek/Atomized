using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterBear : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float bearSpeed; // speed Waterbear moves at
    [SerializeField] Transform playerPos; // get position of player
    [SerializeField] GameObject playerObj;
    private PlayerBonding pBscript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pBscript = playerObj.GetComponent<PlayerBonding>();
    }

    void Update()
    {
        // water bear constantly follows player at a certain speed
        Vector2 direction = (playerPos.position - transform.position).normalized;
        Vector2 newPosition = rb.position + direction * bearSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);

        // rotation that makes Waterbear sprite head always face player
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Waterbear scales in size over time
        this.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Water Bear 'kills' player and ends game
            // eventually make it show death screen/pause current scene until player either restarts/goes to menu
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Water Bear destroys atoms in its way
        if (other.gameObject.CompareTag("Atom"))
        {
            Destroy(other.gameObject);

            // check if atom was bonded to Player, and remove it from list/decrease count from total
            AtomBonding aBscript = other.gameObject.GetComponent<AtomBonding>();
            if (aBscript.atomBonded)
            {
                pBscript.currAtoms.Remove(other.gameObject);

                if (other.gameObject.name == "Hydrogen Atom")
                {
                    pBscript.totalHydrogen -= 1;
                }

                if (other.gameObject.name == "Oxygen Atom")
                {
                    pBscript.totalOxygen -= 1;
                }
            }
        }
    }
}
