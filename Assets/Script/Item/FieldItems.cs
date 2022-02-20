using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    // public Item item;
    public Skill skill;
    // public SpriteRenderer image;

    public void SetItem(Skill skill)
    {
        this.skill = skill;
        this.GetComponent<SpriteRenderer>().sprite = this.skill.image;
        //item.itemName = _item.itemName;
        //item.itemImage = _item.itemImage;
        //item.itemType = _item.itemType;
        //image.sprite = item.itemImage;
    }

    public Skill GetItem()
    {
        return skill;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
