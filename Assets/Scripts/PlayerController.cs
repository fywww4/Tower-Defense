using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    public GameObject bombPrefab;
    public Transform bombSpawnPoint;

    public float playerHealth = 100f;

    public Text healthText;

    public int maxBombs = 3;
    private int currentBombs = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 移動輸入
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 放炸彈
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceBomb();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * moveSpeed;
    }

    void PlaceBomb()
    {
        Instantiate(
            bombPrefab,
            transform.position,
            Quaternion.identity
        );
    }

    // 玩家受傷
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;

        Debug.Log("Player HP: " + playerHealth);

        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }

        healthText.text =
            "PLAYER HP : " + playerHealth;

    }
}