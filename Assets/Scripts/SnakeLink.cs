using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeLink : AbstractSnakeLink
{
    private SnakeLink m_previousPiece;
    public SnakeLink PreviousPiece { get { return m_previousPiece; } set { m_previousPiece = value; } }

    protected override bool IsMoving { get { return (m_previousPiece) ? m_previousPiece.IsMoving : false; } }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Trail"))
            return;

        if (!IsTurning)
            TurnMotion(other.transform.forward);

        if (NextPiece == null)
            Destroy(other);
    }
}

public abstract class AbstractSnakeLink : MonoBehaviour
{
    
    [SerializeField]
    private float m_maxSpeed;
    protected float MaxSpeed { get { return m_maxSpeed; } }
    [SerializeField]
    private float m_acceleration;
    protected float Acceleration { get { return m_acceleration; } }
    [SerializeField]
    private float m_turningTime;
    protected float TurningTime { get { return m_turningTime; } }
    
    private SnakeLink m_nextPiece;
    protected SnakeLink NextPiece { get { return m_nextPiece; } }
    private bool m_isTurning;
    protected bool IsTurning { get { return m_isTurning; } set { m_isTurning = value; } }

    private Rigidbody m_rigidbody;
    private Vector3 m_cacheDirection = Vector3.forward;
    private Vector3 m_newDirection = Vector3.forward;

    protected abstract bool IsMoving { get; }

    // Use this for initialization
    void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (IsMoving)
            m_rigidbody.AddRelativeForce(Vector3.forward * Acceleration);

        Vector3.ClampMagnitude(m_rigidbody.velocity, MaxSpeed);
    }

    protected void TurnMotion(Vector3 direction)
    {
        m_newDirection = direction;
        m_isTurning = true;

        LeanTween.value(gameObject, OnUpdateTurn, 0f, 1f, 0.25f).
                setOnComplete(OnCompleteTurn);
    }
    private void OnUpdateTurn(float motionValue)
    {
        transform.forward = Vector3.Lerp(m_cacheDirection, m_newDirection, motionValue);
    }
    private void OnCompleteTurn()
    {
        m_cacheDirection = transform.forward;
        m_isTurning = false;
    }
}

public class TurningEvent : UnityEvent<Vector3> { }