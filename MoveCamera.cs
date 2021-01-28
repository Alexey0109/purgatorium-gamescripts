using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform character;
    private Vector2 offset;

    private const float dumping = 10f;
    private bool isLeft;
    private int lastX;

    private void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }
    private void Update()
    {
        int currentX = Mathf.RoundToInt(character.position.x);
        if (currentX > lastX)
        {
            isLeft = false;
        }
        else if (currentX < lastX)
        {
            isLeft = true;
        }
        lastX = Mathf.RoundToInt(character.position.x);

        Vector2 target;
        if (isLeft)
        {
            target = new Vector2(character.position.x - offset.x, character.position.y - offset.y);
        }
        else
        {
            target = new Vector2(character.position.x + offset.x, character.position.y + offset.y);
        }
        transform.position = Vector2.Lerp(character.position, target, dumping * Time.deltaTime);
    }

    private void FindPlayer(bool characterIsLeft)
    {
        lastX = Mathf.RoundToInt(character.position.x);
        if (characterIsLeft)
        {
            transform.position = new Vector2(character.position.x - offset.x, character.position.y - offset.y);
        }
        else
        {
            transform.position = new Vector2(character.position.x + offset.x, character.position.y + offset.y);
        }
    }
}
