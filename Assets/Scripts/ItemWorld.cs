using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;
public class ItemWorld : MonoBehaviour
{
  public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
  {
    Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

    ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
    itemWorld.SetItem(item);

    return itemWorld;
  }
  private Item item;
  private SpriteRenderer spriteRenderer;
  private TextMeshProUGUI textMeshPro;
  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    textMeshPro = GameObject.Find("amountText").GetComponent<TextMeshProUGUI>();
  }

  public static ItemWorld DropItem(Vector3 dropPosition, Item item)
  {
    Vector3 randomDir = UtilsClass.GetRandomDir();
    ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir, item);
    itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir.normalized * 5f, ForceMode2D.Force);
    return itemWorld;
  }
  public void SetItem(Item item)
  {
    this.item = item;
    spriteRenderer.sprite = item.GetSprite();
    if (item.amount > 1)
      textMeshPro.SetText(item.amount.ToString());
    else
      textMeshPro.SetText("");
  }

  public Item GetItem()
  {
    return item;
  }

  public void DestroySelf()
  {
    Destroy(gameObject);
  }
}
