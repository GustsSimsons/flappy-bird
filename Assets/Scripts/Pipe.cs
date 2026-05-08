using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float speed = 2f;
    public float destroyX = -10f;
    void Update()
    {
        transform.position += Vector3.left * speed  * Time.deltaTime;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
