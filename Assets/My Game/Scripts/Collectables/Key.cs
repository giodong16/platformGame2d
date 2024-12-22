using System.Collections;
using UnityEngine;

public class Key : MonoBehaviour
{
    public float moveSpeed = 5f;     // Tốc độ di chuyển của item
    public float disappearDelay = 0.5f;  // Thời gian delay trước khi item biến mất sau khi đến vị trí cố định
    public Transform targetPosition;  // Vị trí mà item sẽ di chuyển đến
    private bool isCollected = false;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected) // Kiểm tra va chạm với người chơi
        {
            isCollected = true;
            if (AudioManager.Instance)
            {
                AudioManager.Instance.PlaySFX(NameSound.CollectKey.ToString());
            }
            if (GameManager.Instance)
            {
                GameManager.Instance.IsHadKey = true;
            }
            StartCoroutine(MoveToTargetAndDisappear());
        }
    }

    IEnumerator MoveToTargetAndDisappear()
    {
       
        while (Vector2.Distance(transform.position, targetPosition.position) > 0.03f) // Di chuyển item đến vị trí cố định
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.1f); // Đợi một chút trước khi biến mất
       
        if (GUIManager.Instance != null)
        {
            GUIManager.Instance.ShowKey(true);
        }
      //  yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
  

}
