using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeHead : AbstractSnakeLink
{
    protected override bool IsMoving { get { return Input.GetKey(KeyCode.UpArrow); } }

    private void Update()
    {
        if (IsTurning)
            return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Instantiate(TrailPrefab, transform.position, transform.rotation);
            TurnMotion(-transform.right);
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Instantiate(TrailPrefab, transform.position, transform.rotation);
            TurnMotion(transform.right);
            return;
        }
    }
}
