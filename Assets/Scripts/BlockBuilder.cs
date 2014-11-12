using UnityEngine;
using System.Collections;
using SimpleJSON;

public enum Sides
{
    Front,Left,Back,Right,Top,Bottom
}

public class BlockBuilder : MonoBehaviour
{
    public Transform Block;
    public int NumberOfStacks = 4;
    public float DistanceBetweenStacks = 2.0f;
    public int StackHeight = 10;
    public float RotationVariation = 1.0f;
    public TextAsset Atlas;

    private JSONNode AtlasNode;
    private float AtlasWidth = 1.0f;
    private float AtlasHeight = 1.0f;

	// Use this for initialization
	void Start ()
    {
        //Load Texture Atlas data
        AtlasNode = JSON.Parse(Atlas.text);
        var size = AtlasNode["meta"]["size"];
        AtlasWidth = size["w"].AsFloat;
        AtlasHeight = size["h"].AsFloat;

        for (int x = 0; x < NumberOfStacks; x++)
        {
            for (int i = 0; i < StackHeight; i++)
            {
                var obj = (Transform)Instantiate(Block, new Vector3(x * DistanceBetweenStacks, 0.5f + i, 0), Quaternion.AngleAxis(Random.value * RotationVariation, new Vector3(0, 1, 0)));
                var mesh = (obj.GetComponent("MeshFilter") as MeshFilter).mesh;


                ApplyUV(mesh, GetRandomFrame(), Sides.Front);
                ApplyUV(mesh, GetRandomFrame(), Sides.Back);
                ApplyUV(mesh, GetRandomFrame(), Sides.Left);
                ApplyUV(mesh, GetRandomFrame(), Sides.Right);

            }
        }

	}

    // Update is called once per frame
    void Update()
    {
	
	}

    private JSONNode GetRandomFrame()
    {

        int nbr = (int)(Random.value * 7 + 1);
        return AtlasNode["frames"][nbr + ".png"]["frame"];
    }

    private void ApplyUV(Mesh mesh, JSONNode frame, Sides side)
    {
        var uvs = mesh.uv;
        switch (side)
        {
            case Sides.Front:
                uvs[0] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[1] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[2] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat);
                uvs[3] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat);
                break;

            case Sides.Back:
                uvs[10] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat);
                uvs[11] = CreateUV(frame["x"].AsFloat,  frame["y"].AsFloat);
                uvs[6] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[7] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                break;

            case Sides.Left:
                uvs[20] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[22] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[23] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat);
                uvs[21] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat);
                break;

            case Sides.Right:
                uvs[16] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[18] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[19] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat);
                uvs[17] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat);
                break;

            case Sides.Top:
                uvs[8] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[9] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[4] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat);
                uvs[5] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat);
                break;

            case Sides.Bottom:
                uvs[12] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[14] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat + frame["h"].AsFloat);
                uvs[15] = CreateUV(frame["x"].AsFloat, frame["y"].AsFloat);
                uvs[13] = CreateUV(frame["x"].AsFloat + frame["w"].AsFloat, frame["y"].AsFloat);
                break;
        }
        mesh.uv = uvs;
    }

    private Vector2 CreateUV(float x, float y)
    {
        return new Vector2(x / AtlasWidth, 1.0f - y / AtlasHeight);
    }
}
