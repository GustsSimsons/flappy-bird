using UnityEngine;

public class PipeScore : MonoBehaviour
{
    private bool scored = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger hit by: " + col.gameObject.name + " tag: " + col.tag);

        if (scored) return;
        if (col.CompareTag("Player"))
        {
            scored = true;
            ScoreManager.Instance.AddPoint();
        }
    }
}