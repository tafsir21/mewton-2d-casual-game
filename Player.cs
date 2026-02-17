using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody2D player_rb;
    private Camera cam;
    [SerializeField] private Animator playerAnim;


    void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        if (!GameManager.Instance.isGameOver && !GameManager.Instance.isPause)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 touchPos = cam.ScreenToWorldPoint(Input.mousePosition);
                touchPos.z = 0f;

                Vector2 direction = (touchPos - transform.position);

                if (direction.x > 0){
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (direction.x < 0){
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                player_rb.linearVelocity = direction.normalized * moveSpeed;
                playerAnim.SetBool("isRun", true);
            }
            else
            {
                player_rb.linearVelocity = Vector2.zero;
                playerAnim.SetBool("isRun", false);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null  && !obstacle.hasBeenHit)
        {
            if (obstacle.isPoint)
            {
                GameManager.Instance.AddPoint(1);
            }else
            {
                GameManager.Instance.LoseLife(1);
                playerAnim.SetTrigger("getDamage");
            }

            UI_Manager.Instance.Update_UI();
            obstacle.DestroyObstacle();
        }
    }
}
