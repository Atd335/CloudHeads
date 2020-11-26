using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementLocal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
    }

    Vector3 moveDirection;
    float speed = 10;
    RaycastHit2D bodyHit;
    void Mover()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0);
        bodyHit = Physics2D.CircleCast(transform.position+moveDirection * Time.deltaTime * speed, (transform.lossyScale.x/2), Vector2.zero);


        if (bodyHit.collider == null)
        {
            transform.position += moveDirection * Time.deltaTime * speed;
        }
        else
        {
            Debug.Log(bodyHit.point);
            transform.position = new Vector2(transform.position.x, transform.position.y) - ((bodyHit.point - new Vector2(transform.position.x, transform.position.y)) / 100);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) - ((bodyHit.point-new Vector2(transform.position.x,transform.position.y))/100),.5f);
    }
}
