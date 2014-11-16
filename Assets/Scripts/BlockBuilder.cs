using UnityEngine;
using System.Linq;
using System.Collections;

public enum Sides
{
    Front,Left,Back,Right,Top,Bottom
}

public class BlockBuilder : MonoBehaviour
{
    public Transform Block;
    public Transform Selection;
    public int NumberOfStacks = 4;
    public float DistanceBetweenStacks = 2.0f;
    public int StackHeight = 10;
    public float RotationVariation = 1.0f;
    public Texture2D Texture;

    private Rect[] sprites = new Rect[64];

	// Use this for initialization
    void Start()
    {
        //Generating Sprite Texture Coordinates
        for (int i = 0; i < 64; i++)
        {
            sprites[i] = new Rect((float)((i % 8) * 256 + 1), (float)(((i) / 8) * 256 + 1), 254.0f, 254.0f);
            sprites[i] = new Rect((float)((i % 8) * 256 + 1), (float)((i / 8) * 256 + 1), 254, 254);
        }


        for (int x = 0; x < NumberOfStacks; x++)
        {
            for (int i = 0; i < StackHeight; i++)
            {
                var obj = (Transform)Instantiate(Block, new Vector3(x * DistanceBetweenStacks, 0.5f + i, 0), Quaternion.AngleAxis(Random.value * RotationVariation, new Vector3(0, 1, 0)));
                var mesh = (obj.GetComponent("MeshFilter") as MeshFilter).mesh;

                var mr = obj.GetComponent<MeshRenderer>();


                ApplyUV(mesh, GetRandomSprite(), Sides.Front);
                ApplyUV(mesh, GetRandomSprite(), Sides.Back);
                ApplyUV(mesh, GetRandomSprite(), Sides.Left);
                ApplyUV(mesh, GetRandomSprite(), Sides.Right);
                ApplyUV(mesh, 0, Sides.Top);
                ApplyUV(mesh, 0, Sides.Bottom);

                if (i == 0 && x == 0)
                {
                    var s = (Transform)Instantiate(Selection);

                    var localOffset = new Vector3(0, 0, -0.501f);
                    var worldOffset = obj.rotation * localOffset;
                    var spawnPosition = obj.position + worldOffset;

                    s.transform.position = spawnPosition;
                    s.transform.forward = obj.forward;
                    s.transform.parent = obj.transform;
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
	
	}

    private int GetRandomSprite()
    {

        return (int)(Random.value * 7 + 1);
    }

    private void ApplyUV(Mesh mesh, int spriteNbr, Sides side)
    {
        var rect = sprites[spriteNbr];
        var uvs = mesh.uv;
        switch (side)
        {
            case Sides.Front:
                uvs[0] = CreateUV(rect.x, rect.yMax);
                uvs[1] = CreateUV(rect.xMax, rect.yMax);
                uvs[2] = CreateUV(rect.x, rect.y);
                uvs[3] = CreateUV(rect.xMax, rect.y);
                break;

            case Sides.Back:
                uvs[10] = CreateUV(rect.xMax, rect.y);
                uvs[11] = CreateUV(rect.x, rect.y);
                uvs[6] = CreateUV(rect.xMax, rect.yMax);
                uvs[7] = CreateUV(rect.x, rect.yMax);
                break;

            case Sides.Left:
                uvs[20] = CreateUV(rect.x, rect.yMax);
                uvs[22] = CreateUV(rect.xMax, rect.yMax);
                uvs[23] = CreateUV(rect.x, rect.y);
                uvs[21] = CreateUV(rect.xMax, rect.y);
                break;

            case Sides.Right:
                uvs[16] = CreateUV(rect.x, rect.yMax);
                uvs[18] = CreateUV(rect.xMax, rect.yMax);
                uvs[19] = CreateUV(rect.x, rect.y);
                uvs[17] = CreateUV(rect.xMax, rect.y);
                break;

            case Sides.Top:
                uvs[8] = CreateUV(rect.x, rect.y);
                uvs[9] = CreateUV(rect.xMax, rect.y);
                uvs[4] = CreateUV(rect.x, rect.yMax);
                uvs[5] = CreateUV(rect.xMax, rect.yMax);
                break;

            case Sides.Bottom:
                uvs[12] = CreateUV(rect.x, rect.y);
                uvs[14] = CreateUV(rect.xMax, rect.y);
                uvs[15] = CreateUV(rect.x, rect.yMax);
                uvs[13] = CreateUV(rect.xMax, rect.yMax);
                break;
        }
        mesh.uv = uvs;
    }

    private Vector2 CreateUV(float x, float y)
    {
        return new Vector2(x / Texture.width, (Texture.height - y) / Texture.height);
    }
}
