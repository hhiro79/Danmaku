using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
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

    private void Start()
    {
        SetUp();
    }

    public virtual void SetUp()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //画面下に流れていく処理
        transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Missile"))
        {
            ItemEffects();
            DisplayItemText();
        }
    }

    public virtual void ItemEffects()
    {
        
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
}
