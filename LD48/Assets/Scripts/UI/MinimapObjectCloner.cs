using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class MinimapObjectCloner : MonoBehaviour
{
    [SerializeField]
    private Material terrainMaterial;

    // Start is called before the first frame update
    void Start()
    {
        List<SpriteShapeRenderer> shapeRenderers = GetComponentsInChildren<SpriteShapeRenderer>(true).ToList();

        foreach(SpriteShapeRenderer renderer in shapeRenderers)
        {
            GameObject minimapObj = Instantiate(renderer.gameObject);
            SpriteShapeRenderer newRenderer = minimapObj.GetComponent<SpriteShapeRenderer>();
            minimapObj.layer = 14;
            minimapObj.transform.position = renderer.transform.position;
            newRenderer.sharedMaterial = terrainMaterial;
            newRenderer.sortingOrder = renderer.sortingOrder + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
