using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSH_PlayerItem : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp;
    public int mp;
    public Text hpText;
    public Text mpText;
    public AudioSource itemUse;
    public AudioSource itemDrop;
    void Start()
    {
        hpText = SSH_UIManager.Instance.hpText;
        mpText = SSH_UIManager.Instance.mpText;
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = hp.ToString();
        mpText.text = mp.ToString();
        //HP 아이템 사용
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (hp > 0)
            {
                SH_PlayerHP.instance.HP += 10;
                hp--;
                itemUse.Play();
            }
        }

        //MP 아이템 사용
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (mp > 0)
            {
                SH_PlayerMP.instance.MP += 10;
                mp--;
                itemUse.Play();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            SSH_ItemDrop item = other.GetComponent<SSH_ItemDrop>(); 
            switch (item.type)
            {
                case SSH_ItemDrop.Type.HP:
                    hp += item.value;
                    itemDrop.Play();
                    break;
                case SSH_ItemDrop.Type.MP:
                    mp += item.value;
                    itemDrop.Play();
                    break;
            }

            Destroy(other.gameObject);

        }
    }
}
