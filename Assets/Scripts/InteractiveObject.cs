using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public string objectName;

    public int interactionID;
    public Interaction[] interactions;

    public bool goAwayAtStart;

    Vector3 basePosition;

    private bool isTransit = false;
    private bool isUp = false;
    private bool isLeft = false;

    private void Awake()
    {
        basePosition = transform.position;

        if (goAwayAtStart)
        {
            GoAway();
        }
    }


    public void GoAway()
    {
        transform.position = new Vector3(999f, 999f, 999f);
    }

    public void Place()
    {
        transform.position = basePosition;
    }

    public void Vertical()
    {
        if (!isTransit)
        {
            if (isUp)
            {
                isTransit = true;

                transform.DOLocalMoveY(-2, 1)
                .SetRelative()
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    isUp = false;
                    isTransit = false;
                });
            }
            else
            {
                isTransit = true;

                transform.DOLocalMoveY(2, 1)
                .SetRelative()
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    isUp = true;
                    isTransit = false;
                });
            }
        }
    }

    public void Horizontal()
    {
        if (!isTransit)
        {
            if (isLeft)
            {
                isTransit = true;

                transform.DOLocalRotate(new Vector3(0, 180, 0), 1)
                .SetRelative()
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    isLeft = false;
                    isTransit = false;
                });
            }
            else
            {
                isTransit = true;

                transform.DOLocalRotate(new Vector3(0, -180, 0), 1)
                .SetRelative()
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    isLeft = true;
                    isTransit = false;
                });
            }
        }
    }
}
