using MonoWaves.QoL;
using UnityEngine;

public class PlayerSliding : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField, Min(0)] private float _startMomentum = 2f;
    [SerializeField, Min(0)] private float _endMomentum = 0.5f;
    [SerializeField, Min(0)] private float _momentumGain = 3.5f;
    [SerializeField, Min(0)] private float _momentumLose = 5f;

    [field: Header("Info")]
    [field: SerializeField, ReadOnly] public bool IsSliding { get; private set; }
    [field: SerializeField, ReadOnly] public float Momentum { get; private set; }
    [field: SerializeField, ReadOnly] public bool StartedSlidingOnGround { get; private set; }

    private Rigidbody2D _rb => PlayerBase.Singleton.Rigidbody;

    private void Update() 
    {
        if (PlayerBase.Singleton.ShiftPressed && PlayerBase.Singleton.IsTouchingGround) StartedSlidingOnGround = true;

        if (PlayerBase.Singleton.IsShifting)
        {
            if (StartedSlidingOnGround)
            {
                IsSliding = true;
                PlayerBase.Singleton.BlockMoveInputs = true;
    
                float direction = PlayerBase.Singleton.Facing == PlayerFacing.Left ? -1 : 1;
    
                if (Momentum > _endMomentum) Momentum -= Time.deltaTime * _momentumLose;
    
                _rb.AddForce(Mathf.Clamp(Momentum, _endMomentum, _startMomentum) * direction * Vector2.right);
            }
        }
        else if (IsSliding)
        {
            StartedSlidingOnGround = false;
            IsSliding = false;
            PlayerBase.Singleton.BlockMoveInputs = false;
        }

        if (!IsSliding && Momentum < _startMomentum) Momentum += Time.deltaTime * _momentumGain;
    }
}