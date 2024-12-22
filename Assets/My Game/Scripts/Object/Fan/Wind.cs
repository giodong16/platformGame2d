using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float windForce = 15f;  // Lực gió
    public Direction direction;
    public GameObject wind;

    public Lever levelControl;

    Vector2 directionVector = Vector2.up;
    Animator anim;
    bool isTurnOn;
    AudioSource audioSource;
    public bool IsTurnOn { get => isTurnOn; 
        set{ isTurnOn = value; TurnOnOrTurnOff(); }
    }
    private void Awake()
    {

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        IsTurnOn = true;
    }
    private void Update()
    {
        if(levelControl== null) return;
            IsTurnOn =!levelControl.IsTurnOn;

    }
    void TurnOnOrTurnOff()
    {
        anim.SetBool("IsTurnOn", IsTurnOn);
        if(isTurnOn)
        {
            wind.SetActive(true);
            AudioManager.Instance?.PlaySFXLoop(audioSource, audioSource.clip);
        }
        else
        { 
            wind.SetActive(false); 
            if(audioSource!=null) audioSource.Stop();
        }
    }
     


    private void OnTriggerStay2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng có Rigidbody2D để ảnh hưởng
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null && isTurnOn)
        {
            switch (direction)
            {
                case Direction.Left:
                    directionVector =Vector2.left;
                    break;
                case Direction.Right:
                    directionVector = Vector2.right;
                    break;
                case Direction.Down:
                    directionVector = Vector2.down;
                    break;
                default:
                    directionVector = Vector2.up;
                    break;
            }
                
            // Tạo lực gió theo hướng x của quạt
            rb.AddForce(directionVector* windForce);
        }
    }
}
public enum Direction
{
    Right, Left, Up, Down
}