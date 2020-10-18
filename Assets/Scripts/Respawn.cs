using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    List<Vector3> details_pos = new List<Vector3>();
    List<Quaternion> details_rotate = new List<Quaternion>();
    GameObject[] details;
    public GameObject[] motherboards;
    Vector3 pos_motherboard = new Vector3(-4.11f, 1.103f, 0.845f);
    GameObject current_motherboard;
    public GameManager gameManager;//DELETE

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arm"))
        {
            for (int i = 0; i < details.Length; i++)
            {
                details[i].transform.position = details_pos[i];
                details[i].transform.rotation = details_rotate[i];
            }
            RandomMotrherboard();
            gameManager.GetComponent<GameManager>().Restart();//DELETE
        }
    }

    private void Start()
    {
        details = GameObject.FindGameObjectsWithTag("detail");

        foreach (var detail in details)
        {
            details_pos.Add(detail.transform.position);
            details_rotate.Add(detail.transform.rotation);
        }
        current_motherboard = Instantiate(motherboards[Random.Range(0, 4)], pos_motherboard, Quaternion.identity);
    }

    public void RandomMotrherboard()
    {
        Destroy(current_motherboard);
        current_motherboard = Instantiate(motherboards[Random.Range(0, 4)], pos_motherboard, Quaternion.identity);
    }
}