using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    private bool _tap;
    public static bool swipeLeft, swipeRight;
    private bool isDraging;
    private Vector2 _startTouch, _swipeDelta;
    private int _deadzoneLength = 125;
    
    void Update()
    {
        _tap = swipeLeft = swipeRight = false;

        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {
            _tap = true;
            isDraging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }
        

        #endregion

        #region Mobile Inputs

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _tap = true;
                isDraging = true;
                _startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }

        #endregion
        
        
       
        //Distance calculation
        _swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0)
                _swipeDelta = Input.touches[0].position - _startTouch;
            else if (Input.GetMouseButton(0))
                _swipeDelta = (Vector2) Input.mousePosition - _startTouch;
        }
        //Deadzone calculation(did we cross it)

        if (_swipeDelta.magnitude > _deadzoneLength)
        {
            //Which direction ?
            var x = _swipeDelta.x;
            var y = _swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            Reset();
        }
    }
    private void Reset()
    {
        _startTouch = Vector2.zero;
        isDraging = false;
    }
}
