using UnityEngine;

public class MeteorFall : MonoBehaviour
{
    public GameObject meteorGround;
    private bool onceOnly;
    public float xzChange;
    public Vector3 targetLocation;
    private float speed = 20f;
    private GameLogic gen;

    // Start is called before the first frame update
    void Start()
    {
        gen = FindObjectOfType<GameLogic>();
        onceOnly = true;
        targetLocation = new Vector3(transform.position.x - xzChange, transform.position.y - 55f, transform.position.z + Mathf.Abs(xzChange));
    }

    // Update is called once per frame
    void Update()
    {
        speed += 3f * Time.deltaTime;
        var currentLoc = gameObject.GetComponent<Transform>().position;
        transform.position = Vector3.MoveTowards(currentLoc, targetLocation, Time.deltaTime * speed);
    }

    // Check for collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 0 && onceOnly)
        {
            onceOnly = false;
            gen.receiveGameObject(Instantiate<GameObject>(meteorGround, this.transform.position, Quaternion.identity));
            Destroy(this.gameObject);
        }

    }
}
