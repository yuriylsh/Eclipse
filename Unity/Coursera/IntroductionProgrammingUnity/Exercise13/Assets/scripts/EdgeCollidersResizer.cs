using UnityEngine;

public class EdgeCollidersResizer : MonoBehaviour {
	private const float colliderThickness = 4f;
    
    void Start () {
        var camera = Camera.main;
		var colliders = new CameraColliders(transform);
        Resize(camera, colliders);
	}

    public void Resize(Camera camera, CameraColliders colliders)
    {
       
        var halfScreenWidth = Vector2.Distance (camera.ScreenToWorldPoint(Vector2.zero),camera.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        var halfScreenHeight = Vector2.Distance (camera.ScreenToWorldPoint(Vector2.zero),camera.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        ScaleVerticalColliders(colliders, halfScreenHeight * 2f);
        ScaleHorizontalColliders(colliders, halfScreenWidth * 2f);
        SetColliderPosition(colliders, camera, halfScreenHeight, halfScreenWidth);
    }

    private void ScaleVerticalColliders(CameraColliders colliders, float screenHeight) 
        => colliders.Left.transform.localScale = colliders.Right.transform.localScale =  new Vector3(colliderThickness, screenHeight, colliderThickness);

    private void ScaleHorizontalColliders(CameraColliders colliders, float screenWidth) 
        => colliders.Top.transform.localScale = colliders.Bottom.transform.localScale =  new Vector3(screenWidth, colliderThickness, colliderThickness);

    private void SetColliderPosition(CameraColliders colliders, Camera camera, float halfScreenHeight, float halfScreenWidth)
    {
        var cameraPosition = camera.transform.position;
        var z = 0;
        
        var rightTransform = colliders.Right.transform;
        rightTransform.position = new Vector3(cameraPosition.x + halfScreenWidth + rightTransform.localScale.x * 0.5f, cameraPosition.y, z);
        
        var leftTransform = colliders.Left.transform;
        leftTransform.position = new Vector3(cameraPosition.x - halfScreenWidth - leftTransform.localScale.x * 0.5f, cameraPosition.y, z);

        var topTransform = colliders.Top.transform;
        topTransform.position = new Vector3(cameraPosition.x, cameraPosition.y + halfScreenHeight + topTransform.localScale.y * 0.5f, z);

        var bottomTransform = colliders.Bottom.transform;
        bottomTransform.position = new Vector3(cameraPosition.x, cameraPosition.y - halfScreenHeight - bottomTransform.localScale.y * 0.5f, z);
    }
}