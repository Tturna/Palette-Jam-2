using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulePointer : MonoBehaviour
{
    public Camera uiCamera;
    public Transform target;
    Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    void Awake()
    {
        targetPosition = target.position;
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle += 360;
        }
        pointerRectTransform.localEulerAngles = new Vector3(0,0,angle);

        float borderSize = 100f;

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffscreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width  - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize ;
        Debug.Log(isOffscreen + " " + targetPositionScreenPoint);

        if (isOffscreen)
        {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;

            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position =  pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }else
        {
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRectTransform.position =  pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        }
    }
}
