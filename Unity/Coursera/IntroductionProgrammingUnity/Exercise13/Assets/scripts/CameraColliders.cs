using UnityEngine;

public class CameraColliders
{
    public BoxCollider2D Top { get; }
    public BoxCollider2D Bottom { get; }
    public BoxCollider2D Left { get; }
    public BoxCollider2D Right { get; }

    public CameraColliders(Transform parent)
    {
        Top = new GameObject().AddComponent<BoxCollider2D>();
        Top.transform.name = "TopCollider";
        Top.transform.parent = parent;
        
        Bottom = new GameObject().AddComponent<BoxCollider2D>();
        Bottom.transform.name="BottomCollider";
        Bottom.transform.parent = parent;
        

        Left = new GameObject().AddComponent<BoxCollider2D>();
        Left.transform.name="LeftCollider";
        Left.transform.parent = parent;
        

        Right = new GameObject().AddComponent<BoxCollider2D>();
        Right.transform.name="RightCollider";
        Right.transform.parent = parent;
    }
}