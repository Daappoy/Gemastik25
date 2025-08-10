using UnityEngine;

public class missingRobot : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public bool isRobotMissing = false;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isRobotMissing = true;
        }
    }
}
