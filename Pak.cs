using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pak : MonoBehaviour
{

    private new     Rigidbody2D rigidbody;
    public          Text        textbox;
    public          int         BossHP = 100;
    private const   float       jumpForce   =   10f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody   = GetComponent<Rigidbody2D>();
        Jump();
        StartCoroutine(JumpCoroutine());
        //textbox     = GetComponent<Text>();
    }

    IEnumerator JumpCoroutine()
    {
        while(true)
        {
            rigidbody.velocity = new Vector2(10f, rigidbody.velocity.y);
            yield return new WaitForSeconds(3);
            Jump();
        }
   
    }
    
    public void Jump() //Not used
    {
        rigidbody.velocity = Vector2.up * jumpForce;        //Apply jump force
    }

    void BossDeath()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (BossHP <= 0)
        {
            BossDeath();
        }
        textbox.text = "" + BossHP;
    }

    void OnCollisionEnter2D(Collision2D col)   
    {
        if (col.gameObject.tag == taglist.tags.enemy_tag)
        {
            Debug.Log("BRUH");
            BossHP -= 10;
            Destroy(col.gameObject);
        }
    }
}
