using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth2 : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip destroySound;
    public int enemyHP;
    private Slider slider;
    public int scoreValue;
    private ScoreManager sm;
    private StageNorma sn;
    public GameObject[] itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //slider = GameObject.Find("EnemyHPSlider").GetComponent<Slider>();
        //slider.maxValue = enemyHP;
        //slider.value = enemyHP;
        sm = GameObject.FindGameObjectWithTag("StageManager").GetComponent<ScoreManager>();

        //AddDestroyCount
        sn = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageNorma>();
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.CompareTag("Missile")){
            GameObject effect = Instantiate (effectPrefab,
                transform.position, Quaternion.identity)
                as GameObject;
            
            Destroy(effect, 0.5f);
            enemyHP -= 1;
            //slider.value = enemyHP;            
            Destroy(col.gameObject);

            if(enemyHP == 0){
                transform.root.gameObject.SetActive(false);
                AudioSource.PlayClipAtPoint(destroySound,
                    transform.position);
                sm.AddScore(scoreValue);

                //StageNormaのAddDestroyCount呼び出し
                sn.AddDestroyCount();
            }

            if(itemPrefab.Length > 0) {
                GameObject dropItem =
                    itemPrefab [Random.Range (0, itemPrefab.Length)];
                Instantiate (dropItem, transform.position,
                    Quaternion.identity);
            }
            

        }
    }
}
