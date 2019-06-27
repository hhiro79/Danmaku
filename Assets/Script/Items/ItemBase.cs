using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public GameObject effectPrefab;
    public AudioClip getSound;
    public GameObject effectTextPrefab;
    public float moveSpeed = 5.0f;

    public enum ItemType
    {
        POWER_UP,
        ONE_UP,
        SHIELD,
        CURE,
        STOP_ATTACK,
        SPECIAL
    }
    public ItemType itemType;

    protected GameObject effect;

    private void Start()
    {
        SetUp();
    }

    public virtual void SetUp()
    {
    }

    public virtual void Update()
    {
        //画面下に流れていく処理
        transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Missile"))
        {
            DisplayItemText();
            ItemEffects();
        }
    }

    private void DisplayItemText()
    {
        GameObject effectText = Instantiate(effectTextPrefab, transform.position, effectTextPrefab.transform.rotation);
        TextMesh textMesh = effectText.GetComponent<TextMesh>();

        switch(itemType){
            case ItemType.CURE:
                textMesh.text = "CURE";
                break;
            case ItemType.ONE_UP:
                textMesh.text = "1 UP !!";
                break;
            case ItemType.POWER_UP:
                textMesh.text = "POWER UP !!";
                break;
            case ItemType.SHIELD:
                textMesh.text = "SHIELD !!";
                break;
            case ItemType.SPECIAL:
                textMesh.text = "SPECIAL MISSILE !!";
                break;
            case ItemType.STOP_ATTACK:
                textMesh.text = "STOP ATTACK";
                break;
        }

        Destroy(effectText, 1.5f);
    }

    public virtual void ItemEffects()
    {
        //エフェクトとSEを発生
        effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(getSound, Camera.main.transform.position, 0.4f);
    }

    public void DestroyItem()
    {
        //アイテムとエフェクトを画面から消す
        Destroy(this.gameObject);
        Destroy(effect, 2.0f);
    }
}
