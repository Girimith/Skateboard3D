using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 targetPos;
    public float speed;

    private void Start()
    {
        transform.DOLocalMove(targetPos, speed).OnComplete(() => transform.DOLocalMove(startPos, speed))
            .SetLoops(-1, LoopType.Yoyo);
            
    }

}
