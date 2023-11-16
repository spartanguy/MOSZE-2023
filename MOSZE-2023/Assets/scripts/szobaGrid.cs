using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class szobaGrid : MonoBehaviour
{
    [SerializeField] private int sor, oszl;
    public GameObject tile;

    private void OnTriggerEnter2D(Collider2D other){

        if (other.gameObject.CompareTag("jatekos")){
            for (int i = 0; i < sor; i++){
                for (int j = 0; j < oszl; j++){
                    var spawnnedTile = Instantiate(tile, new Vector3(i+7.5f,j-2.5f) , Quaternion.identity);
                    spawnnedTile.name = $"Tile {i} {j}";
                }
            } 
        }
    }
}
