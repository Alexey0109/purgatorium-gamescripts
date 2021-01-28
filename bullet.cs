using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using taglist;

public class bullet : MonoBehaviour
{
    private new Rigidbody2D rb;

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    void RotateView(Vector2 diff)
    {
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(DestroyCoroutine());
    }

    void Update()
    {
        float myspeed = 2f;
        Vector2 diff = GameObject.Find(taglist.tags.player_tag).transform.position;
        RotateView(diff);
        rb.AddForce(diff * myspeed);
    }
}
