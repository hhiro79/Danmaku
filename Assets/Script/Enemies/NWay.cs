using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NWay : MonoBehaviour
{
    public GameObject enemyFireMissilePrefab;

    //何Wayミサイルを発射するか決める
    public int wayNumber;

    // Start is called before the first frame update
    void Start()
    {
        //for文を活用
        for(int i = 0; i < wayNumber; i++){

            GameObject enemyFireMissile = (GameObject) Instantiate (enemyFireMissilePrefab, transform.position, Quaternion.Euler (0, 7.5f - (7.5f * wayNumber) + (15 * i), 0));

            //SetParent()は親子関係を作るメソッド
            //このスクリプトが付いているNWayオブジェクトをenemyFireMissileクローンの親に設定
            enemyFireMissile.transform.SetParent(this.gameObject.transform);
        }
    }
}
