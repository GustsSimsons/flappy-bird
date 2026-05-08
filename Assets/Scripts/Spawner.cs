using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pipePrefab;

    public float gap = 3.5f;
    public float spawnRate = 2f;
    public float edgePadding = 0.5f;

    public float groundY = -2.5f;
    public float ceilingY = 4.3f;

    public float pipeLength = 10f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnPipes();
            timer = 0f;
        }
    }

    void SpawnPipes()
    {
        float gapMin = groundY + gap / 2 + edgePadding;
        float gapMax = ceilingY - gap / 2 - edgePadding;
        float gapCenter = Random.Range(gapMin, gapMax);

        float bottomCapY = gapCenter - gap / 2;
        float topCapY = gapCenter + gap / 2; 

        GameObject bottom = Instantiate(pipePrefab, transform);
        bottom.transform.position = new Vector3(
            transform.position.x,
            bottomCapY - pipeLength / 2f,
            0
        );
        bottom.transform.rotation = Quaternion.identity;

        GameObject top = Instantiate(pipePrefab, transform);
        top.transform.position = new Vector3(
            transform.position.x,
            topCapY + pipeLength / 2f,
            0
        );
        top.transform.rotation = Quaternion.Euler(0, 0, 180f);

        bottom.transform.localScale = Vector3.one;
        top.transform.localScale = Vector3.one;


        GameObject scorer = new GameObject("ScoreTrigger");
        scorer.transform.position = new Vector3(transform.position.x, gapCenter, 0);
        Debug.Log("Scorer spawned at: " + scorer.transform.position + " gapCenter: " + gapCenter);
        scorer.tag = "ScoreTrigger";
        scorer.transform.parent = bottom.transform; 
        scorer.transform.localScale = Vector3.one;

        BoxCollider2D col = scorer.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = new Vector2(0.5f, gap);

        scorer.AddComponent<PipeScore>();


    }
}