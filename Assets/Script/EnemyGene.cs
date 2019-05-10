using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGene : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    [SerializeField, HeaderAttribute("敵の発生場所")]
    public Vector3 geneValues;
    [SerializeField, HeaderAttribute("敵を何体発生させるか")]
    public int geneCounts;

    [SerializeField, HeaderAttribute("敵を最初に発生させるまでの待機時間")]
    public float startWait;
    [SerializeField, HeaderAttribute("敵を発生させる時間間隔")]
    public float geneWait;
    [SerializeField, HeaderAttribute("一定数の敵を発生させたのち、次の発生開始までの待機時間")]
    public float waveWait;

    // Start is called before the first frame update
    void Start()
    {
        //GeneWavesコルーチンを作動させる
        StartCoroutine (GeneWaves());
    }

    IEnumerator GeneWaves(){

        //startWaitで指定した時間分だけ待機して以下の処理を実行
        yield return new WaitForSeconds (startWait);

        while (true){
            for(int i = 0; i < geneCounts; i++){

                //発生させる敵をランダムに選ぶ
                GameObject enemy =
                    enemyPrefabs[Random.Range (0, enemyPrefabs.Length)];
                
                //敵を発生させる装置の位置をランダムに設定
                Vector3 enemyGenePosition = new Vector3
                    (Random.Range (-geneValues.x, geneValues.x),
                    geneValues.y, geneValues.z);
                
                //敵を発生させる
                Instantiate (enemy, enemyGenePosition, Quaternion.identity);

                //敵を生み出す時間間隔の調整
                //geneWaitで設定した時間分だけ、for文の繰り返し処理を一時中断
                yield return new WaitForSeconds(geneWait);
            }

            //for文の処理が終了した後、次のfor文処理が開始されるまでの待機時間
            //再び敵を生み出し始めるまでの待機時間
            yield return new WaitForSeconds(waveWait);
        }
    }
}
