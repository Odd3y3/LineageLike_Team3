using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItmeType
{
    Itme,Gold
}

public class ItemDrop : MonoBehaviour
{
    public ItmeType Type;
    // Start is called before the first frame update
    public Item myItem = null;
    public Item.EQUIPMENGRADE curItemGrade;
    public uint Gold = 0;

    public GameObject DropItem = null;
    public GameObject ItemSlot = null;
    public GameObject[] myGoldObjects = null;


    public Animator myAnim = null;
    public RuntimeAnimatorController myAnimController = null;

    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSetItem(Transform tra)
    {
        GoldSetting();
        DroppingItem(tra);
    }

    public void ItemSetting()
    {
        myItem = GameManager.Inst.ItemManager.ItemList.Items[Random.Range(0, GameManager.Inst.ItemManager.ItemList.Items.Count)];
        GameManager.Inst.ItemManager.OnRandomSetItemGrade(myItem);
        curItemGrade = myItem.ItemGrade;
        DropItem = myItem.Object;
    }

    public void GoldSetting()
    {
        Gold = (uint)Random.Range(0, 101);
        if (Gold < 50)
        {
            DropItem = myGoldObjects[0];
        }
        else
        {
            DropItem = myGoldObjects[1];
        }
    }

    public void DroppingItem(Transform tra)
    {
        StartCoroutine(Delay(tra));
    }

    IEnumerator Delay(Transform tra)
    {
        GameObject obj = Instantiate(DropItem, this.transform);
        obj.transform.localPosition = tra.position;
        StartCoroutine(UpDowning(obj));
        yield return new WaitForSeconds(1.0f);
        OnDropSetting(obj);
    }


    public void OnDropSetting(GameObject obj)
    {
        OnAnimationSetting(obj);
        OnColliderSetting(obj);
        obj.AddComponent<AcquisitionItem>();
    }

    public void OnAnimationSetting(GameObject obj)
    {
        myAnim =obj.AddComponent<Animator>();
        obj.GetComponent<Animator>().runtimeAnimatorController =myAnimController;
        myAnim.SetBool("IsDrop", true); 
    }

    public void OnColliderSetting(GameObject obj)
    {
        obj.AddComponent<SphereCollider>();
        obj.GetComponent<SphereCollider>().radius = 1.0f;
        obj.GetComponent<SphereCollider>().isTrigger = true;
    }

    public void ChangeItem()
    {
        myItem = GameManager.Inst.ItemManager.ItemList.Items[Random.Range(0, GameManager.Inst.ItemManager.ItemList.Items.Count)];
        GameManager.Inst.ItemManager.OnRandomSetItemGrade(myItem);
        curItemGrade = myItem.ItemGrade;
    }

    float ToTalDist = 0.0f;

  IEnumerator UpDowning(GameObject obj)
    {
        Vector3 dir = new Vector3(0, 2, 0);
        float dist = 2.0f;

        dir = Vector3.up;
        ToTalDist = dist = dir.magnitude;
        dir.Normalize();

        while (true)
        {
            float delta = 1.0f * Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }

            dist -= delta;


            if (Mathf.Approximately(dist, 0.0f))
            {
                dir = -dir;
                dist = ToTalDist;
            }

            //transform.position = transform.position + dir * delta; 
            transform.Translate(dir * delta);

            //transform.Rotate(0, 360.0f * Time.deltaTime, 0);
            //transform.Rotate(Vector3.up * 360.0f * Time.deltaTime);//√ ¥Á 360µµ


            yield return null;  
        }
    }


    public void AcquisitionItem(Collider other)
    {
        if (other.GetComponent<Player>().myItem.Count < GameManager.Inst.UiManager.myInventory.slots.Length && myItem != null)
        {
            other.GetComponent<Player>().myItem.Add(myItem);
            other.GetComponent<Player>().PickUpItem = myItem;
            other.GetComponent<Player>().OnAcquisition(myItem);
            Destroy(gameObject);
        }
        else
        {
            GameManager.Inst.inGameManager.GoldDrop(Gold);
            Destroy(gameObject);
        }
    }

}
