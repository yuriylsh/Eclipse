using UnityEngine;

public class Person : MonoBehaviour {
	void Start () => ApplyRandomForce();

    private void ApplyRandomForce() => GetComponent<Rigidbody2D>().AddForce(RandomForce, ForceMode2D.Impulse);

    private Vector2 RandomForce
    {
        get
        {
            var angle = Random.Range(0, 2 * Mathf.PI);
            var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            var magnitude = Random.Range(5f, 15f);
            return direction * magnitude;
        }
    }
}
