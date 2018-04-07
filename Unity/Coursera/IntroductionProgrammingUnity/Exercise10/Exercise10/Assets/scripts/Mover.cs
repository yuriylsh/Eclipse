using UnityEngine;

public class Mover : MonoBehaviour
{

	void Start () => GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1f), ForceMode2D.Impulse);
}
