using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private Transform m_snakeHead;
    [SerializeField]
    private Transform m_snakeBody;
    [SerializeField]
    private GameObject m_snakeTrail;
    protected GameObject SnakeTrail { get { return m_snakeTrail; } }
}
