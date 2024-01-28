using TMPro;
using UnityEngine;

public class DamageSkin : MonoBehaviour
{
    TextMeshPro textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.alpha -= 0.75f * Time.deltaTime;
        textMesh.transform.position += new Vector3(0.5f, 6f) * Time.deltaTime;
        if (textMesh.alpha < 0.05f)
        {
            Destroy(transform.gameObject);
        }
    }
}
