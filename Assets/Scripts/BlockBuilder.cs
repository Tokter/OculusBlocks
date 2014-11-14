using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEditor;

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

    private Sprite[] sprites;

	// Use this for initialization
	void Start ()
    {
        string spriteSheet = AssetDatabase.GetAssetPath(Texture);
        sprites = AssetDatabase.LoadAllAssetsAtPath(spriteSheet).OfType<Sprite>().ToArray();

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

                if (i==0 && x==0)
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
        var rect = sprites[spriteNbr].textureRect;
        var uvs = mesh.uv;
        switch (side)
        {
            case Sides.Front:
                uvs[0] = CreateUV(rect.x, rect.y);
                uvs[1] = CreateUV(rect.xMax, rect.y);
                uvs[2] = CreateUV(rect.x, rect.yMax);
                uvs[3] = CreateUV(rect.xMax, rect.yMax);
                break;

            case Sides.Back:
                uvs[10] = CreateUV(rect.xMax, rect.yMax);
                uvs[11] = CreateUV(rect.x, rect.yMax);
                uvs[6] = CreateUV(rect.xMax, rect.y);
                uvs[7] = CreateUV(rect.x, rect.y);
                break;

            case Sides.Left:
                uvs[20] = CreateUV(rect.x, rect.y);
                uvs[22] = CreateUV(rect.xMax, rect.y);
                uvs[23] = CreateUV(rect.x, rect.yMax);
                uvs[21] = CreateUV(rect.xMax, rect.yMax);
                break;

            case Sides.Right:
                uvs[16] = CreateUV(rect.x, rect.y);
                uvs[18] = CreateUV(rect.xMax, rect.y);
                uvs[19] = CreateUV(rect.x, rect.yMax);
                uvs[17] = CreateUV(rect.xMax, rect.yMax);
                break;

            case Sides.Top:
                uvs[8] = CreateUV(rect.x, rect.yMax);
                uvs[9] = CreateUV(rect.xMax, rect.yMax);
                uvs[4] = CreateUV(rect.x, rect.y);
                uvs[5] = CreateUV(rect.xMax, rect.y);
                break;

            case Sides.Bottom:
                uvs[12] = CreateUV(rect.x, rect.yMax);
                uvs[14] = CreateUV(rect.xMax, rect.yMax);
                uvs[15] = CreateUV(rect.x, rect.y);
                uvs[13] = CreateUV(rect.xMax, rect.y);
                break;
        }
        mesh.uv = uvs;
    }

    private Vector2 CreateUV(float x, float y)
    {
        return new Vector2(x / Texture.width, y / Texture.height);
    }
}
