using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class BeasItem : MonoBehaviour
{
    public ItemObject item;
    private BoxCollider2D collider2d;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<BoxCollider2D>();
    }

    public void Start()
    {
        sprite.sprite = item.uiDisplay;
        sprite.sortingOrder = 10;
        collider2d.size = new Vector2(0.5f, 0.6f);
        collider2d.isTrigger = true;
    }
}
