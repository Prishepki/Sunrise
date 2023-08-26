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
        if (PlayerBase.Singleton.ShiftPressed && PlayerBase.Singleton.IsTouchingGround) 
        {
            StartedSlidingOnGround = true;
            PlayerSprite.Singleton.MotionExecutor.InvokeParameter("isSlidingAwake", true, false);
        }
        
        if (PlayerBase.Singleton.IsShifting)
        {
            if (StartedSlidingOnGround)
            {
                IsSliding = true;

                PlayerBase.Singleton.BlockMoveInputs = true;
                PlayerBase.Singleton.BlockJumpInputs = true;
    
                if (Momentum > _endMomentum) Momentum -= Time.deltaTime * _momentumLose;
            }

            if (PlayerBase.Singleton.IsTouchingWall && IsSliding)
            {
                StopSliding();
            }
        }
        else if (IsSliding)
        {
            StopSliding();
        }

        if (!IsSliding && Momentum < _startMomentum) Momentum += Time.deltaTime * _momentumGain;

        PlayerBase.Singleton.IsSliding = IsSliding;
    }

    private void StopSliding()
    {
        PlayerSprite.Singleton.MotionExecutor.InvokeParameter("isSlidingStop", true, false);

        StartedSlidingOnGround = false;
        IsSliding = false;

        PlayerBase.Singleton.BlockMoveInputs = false;
        PlayerBase.Singleton.BlockJumpInputs = false;
    }

    private void FixedUpdate() 
    {
        if (IsSliding)
        {
            float direction = PlayerBase.Singleton.Facing == PlayerFacing.Left ? -1 : 1;
            Vector2 moveDirection = PlayerBase.Singleton.IsSloped() ? ZVector2Math.ProjectOnPlane(Vector2.right, PlayerBase.Singleton.SlopeNormal) : Vector2.right;
        
            _rb.AddForce(Mathf.Clamp(Momentum, _endMomentum, _startMomentum) * 5 * direction * moveDirection);
        }
    }
}
