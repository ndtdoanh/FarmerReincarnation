using UnityEngine;
using System.Collections;

public class NPCMovement : MonoBehaviour
{
    public Transform[] waypoints; // Mảng các điểm đến
    public float moveSpeed = 2f;  // Tốc độ di chuyển của NPC
    public float waitTime = 60f;  // Thời gian chờ tại mỗi điểm đến

    private int currentWaypointIndex = 0; // Chỉ số của điểm đến hiện tại
    private Animator animator; // Tham chiếu đến Animator

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string LastHorizontal = "LastHorizontal";
    private const string LastVertical = "LastVertical";

    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy Animator từ NPC
        StartCoroutine(MoveToNextWaypoint());
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            // Di chuyển đến điểm tiếp theo
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            while (Vector2.Distance(transform.position, targetWaypoint.position) > 0.1f)
            {
                Vector3 direction = targetWaypoint.position - transform.position;
                direction.Normalize();

                // Cập nhật Animator Parameters
                animator.SetFloat(Horizontal, direction.x);
                animator.SetFloat(Vertical, direction.y);
                if (direction != Vector3.zero) //dòng này là khi không di chuyển thì hướng nhìn sẽ ở đó
                {
                    animator.SetFloat(LastHorizontal, direction.x);
                    animator.SetFloat(LastVertical, direction.y);
                }

                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Reset Animator Parameters khi dừng lại
            animator.SetFloat(Horizontal, 0);
            animator.SetFloat(Vertical, 0);

            // Chờ tại điểm đến
            yield return new WaitForSeconds(waitTime);

            // Chuyển đến điểm tiếp theo trong mảng
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
