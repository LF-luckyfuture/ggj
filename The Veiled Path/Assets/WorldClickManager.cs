// WorldClickManager.cs
using UnityEngine;

public class WorldClickManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                var interact = hit.collider.GetComponent<SceneInteractPoint>();
                if (interact != null)
                {
                    interact.Trigger();
                }
            }
        }
    }
}