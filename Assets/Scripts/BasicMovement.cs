using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public string playerName;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var inputXAxis = Input.GetAxis($"Horizontal_{playerName}");
        var inputYAxis = Input.GetAxis($"Vertical_{playerName}");

        MoveXY(inputXAxis, inputYAxis);
        AnimateMove(inputXAxis, inputYAxis);
    }

    private void AnimateMove(float inputXAxis, float inputYAxis)
    {
        animator.SetFloat("Horizontal", inputXAxis);
        animator.SetFloat("Vertical", inputYAxis);
    }

    private void MoveXY(float inputXAxis, float inputYAxis)
    {
        Vector3 horizontal = new Vector3(inputXAxis, 0.0f, 0.0f);
        var position = transform.position;

        position = position + horizontal * Time.deltaTime;
        Vector3 vertical = new Vector3(0.0f, inputYAxis, 0.0f);
        position = position + vertical * Time.deltaTime;
        transform.position = position;
    }
}