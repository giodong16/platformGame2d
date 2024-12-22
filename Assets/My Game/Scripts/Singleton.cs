using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Instance duy nhất của Singleton
    private static T instance;

    // Đảm bảo truy cập thread-safe khi tạo instance lần đầu
    private static object _lock = new object();

    // Kiểm tra nếu Singleton sẽ bị hủy khi scene kết thúc
    private static bool applicationIsQuitting = false;

    // Public property để truy cập instance
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' đã bị hủy khi game đang thoát. Sẽ không tạo lại.");
                return null;
            }

            lock (_lock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Có nhiều hơn một instance của Singleton '" + typeof(T) + "'! Sẽ chỉ giữ instance đầu tiên.");
                        return instance;
                    }

                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = "(singleton) " + typeof(T).ToString();

                        // Đảm bảo không phá hủy instance khi load scene mới
                        DontDestroyOnLoad(singletonObject);

                        Debug.Log("[Singleton] Tạo instance mới của " + typeof(T) + ".");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Sử dụng instance đã tồn tại của " + typeof(T) + ".");
                    }
                }

                return instance;
            }
        }
    }

    // Đảm bảo Singleton không bị phá hủy khi load scene khác
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Nếu đã có instance khác, hủy đối tượng này
        }
    }

    // Đánh dấu khi game đang thoát để không tạo lại instance
    private void OnDestroy()
    {
        if (instance == this)
        {
            applicationIsQuitting = true;
        }
    }
}
