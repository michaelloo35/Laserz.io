using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform[] targets = new Transform[0];
    public Camera cam;

    private static float ZOOM_SPEED = 0.7f;
    private static float MAX_ZOOM = 2.1f;
    [SerializeField] int bufferOut = 100;
    [SerializeField] int bufferIn = 140;

    public void Initialize(Transform[] playerPositions)
    {
        targets = playerPositions;
    }

    // Update is called once per frame
    void Update()
    {
        if (targets.Length > 0)
            cam.transform.position += (getAveragePosition(targets) - cam.transform.position) * Time.deltaTime;
        
        cameraZoom();
    }

    private void cameraZoom()
    {
        bool outside = false;
        Vector3 vp = Vector3.zero;
        for (int i = 0; i < targets.Length; ++i)
        {
            if (targets[i].GetComponent<Renderer>().isVisible)
            {
                vp = Camera.main.WorldToScreenPoint(targets[i].position);
                if (vp.x < bufferOut || vp.x > Screen.width - bufferOut || vp.y < bufferOut ||
                    vp.y > Screen.height - bufferOut)
                {
                    outside = true;
                    break;
                }

                continue;
            }

            outside = true;
            break;
        }

        if (outside)
        {
            cam.orthographicSize += ZOOM_SPEED * Time.deltaTime;
        }
        else
        {
            int countIn = 0;
            int cnt = targets.Length;
            for (int i = 0; i < cnt; ++i)
            {
                vp = Camera.main.WorldToScreenPoint(targets[i].position);
                if (vp.x > bufferIn && vp.x < Screen.width - bufferIn && vp.y > bufferIn &&
                    vp.y < Screen.height - bufferIn) ++countIn;
            }

            if (countIn == cnt && cam.orthographicSize > MAX_ZOOM)
                cam.orthographicSize -= ZOOM_SPEED * Time.deltaTime;
        }
    }


    private Vector3 getAveragePosition(Transform[] positions)
    {
        Vector3 average = new Vector3(0, 0, 0);

        foreach (var position in positions)
        {
            average += position.position;
        }

        average /= positions.Length;
        average.z = -10;
        return average;
    }
}