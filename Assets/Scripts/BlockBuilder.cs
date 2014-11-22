using UnityEngine;
using System.Linq;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;


public class BlockBuilder : MonoBehaviour
{
    public Transform BlockPrefab;
    public int NumberOfStacks = 4;
    public float DistanceBetweenStacks = 2.0f;
    public int StackHeight = 10;
    public float RotationVariation = 1.0f;

    private static Dictionary<int, Block> blocks = new Dictionary<int, Block>();
    

	// Use this for initialization
    void Start()
    {

        //Generate Blocks
        for (int x = 0; x < NumberOfStacks; x++)
        {
            for (int i = 0; i < StackHeight; i++)
            {
                var block = new Block(BlockPrefab, new Vector3(x * DistanceBetweenStacks, 0.5f + i, 0), Quaternion.AngleAxis(Random.value * RotationVariation, new Vector3(0, 1, 0)));
                blocks.Add(block.Object.GetInstanceID(), block);
                
                
                //if (i == 0 && x == 0)
                //{
                //    var s = (Transform)Instantiate(SelectionObject);

                //    var localOffset = new Vector3(0, 0, -0.501f);
                //    var worldOffset = obj.rotation * localOffset;
                //    var spawnPosition = obj.position + worldOffset;

                //    s.transform.position = spawnPosition;
                //    s.transform.forward = obj.forward;
                //    s.transform.parent = obj.transform;
                //}
            }
        }

    }

    public static void Select(RaycastHit hit)
    {
        var selection = hit.collider.gameObject;

        selection.renderer.enabled = !selection.renderer.enabled;
        var effect = selection.transform.GetChild(0).gameObject;
        effect.renderer.enabled = selection.renderer.enabled;
    }

    // Update is called once per frame
    void Update()
    {
	}




}
