using UnityEngine;

public class CameraRnder : MonoBehaviour
{
    private ComputeShader shader;
    private RenderTexture texture;
    public Transform player;
    private const int RAYS_HORIZONTAL = 360;
    private const int RAYS_VERTICAL = 180;
    Vector3[][] rays = new Vector3[RAYS_VERTICAL][];
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        for (int i = 0; i < RAYS_VERTICAL; i++)
        {
            rays[i] = new Vector3[RAYS_HORIZONTAL];
        }

        for (int verticalRayAngle = 0; verticalRayAngle < RAYS_VERTICAL; verticalRayAngle++)
        {
            for (int horizontalRayAngle = 0; horizontalRayAngle < RAYS_HORIZONTAL; horizontalRayAngle++)
            {
                Vector3 calculatedVector = Quaternion.AngleAxis(horizontalRayAngle, Vector3.up) * Vector3.forward;
                calculatedVector = Quaternion.AngleAxis(verticalRayAngle, Vector3.forward) * calculatedVector;
                rays[verticalRayAngle][horizontalRayAngle] = calculatedVector;
            }
        }
    }

    private void OnPreRender()
    {
        for (int i = 0; i < gameManager.ControllerList.Count; i++)
        {
            Controller character = gameManager.ControllerList[i];
            if(character.character.spotLight == null) continue;
            if (character.IsPlayer)
            {
                character.character.spotLight.enabled = true;
            }
            else
            {
                character.character.spotLight.enabled = false;

            }
        }
    }
}
