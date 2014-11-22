using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public enum SideType
    {
        Front, Left, Back, Right, Top, Bottom
    }

    public class BlockSide
    {
        public int Type { get; set; }

        public Object Selection { get; set; }

        public bool Selected
        {
            get { if (Selection == null) return false; else return (Selection as Component).GetComponent<SpriteRenderer>().enabled; }
            set { if (Selection!=null) (Selection as Component).GetComponent<SpriteRenderer>().enabled = value; }
        }

        public BlockSide()
        {
            Type = 0;
            Selected = false;
        }
    }

    public class Block
    {
        private Dictionary<SideType, BlockSide> _sides = new Dictionary<SideType, BlockSide>();
        private Object _object;
        private static Rect[] sprites = null;
        private const int TextureSize = 2048;
        private const int NumberOfSymbols = 7;

        public Object Object
        {
            get { return _object; }
        }

        public BlockSide this[SideType side]
        {
            get
            {
                if (!_sides.ContainsKey(side)) _sides.Add(side, new BlockSide());
                return _sides[side];
            }
            set { if (!_sides.ContainsKey(side)) _sides.Add(side, value); else _sides[side] = value; }
        }

        public Block(Transform blockPrefab, Vector3 position, Quaternion rotation)
        {          
            if (sprites == null) //Generating Sprite Texture Coordinates
            {            
                sprites =  new Rect[64];             
                for (int i = 0; i < 64; i++)
                {
                    sprites[i] = new Rect((float)((i % 8) * 256 + 1), (float)((i / 8) * 256 + 1), 254, 254);
                }
            }

            //Generate Block
            _object = Object.Instantiate(blockPrefab, position, rotation);
            var objTransform = _object as Transform;

            this[SideType.Bottom].Type = 0;
            this[SideType.Top].Type = 0;
            this[SideType.Front].Type = GetRandomSprite();
            this[SideType.Back].Type = GetRandomSprite();
            this[SideType.Left].Type = GetRandomSprite();
            this[SideType.Right].Type = GetRandomSprite();

            Apply();
        }

        public void Apply()
        {
            var mesh = ((_object as Component).GetComponent("MeshFilter") as MeshFilter).mesh;
            foreach (var key in _sides.Keys)
            {
                ApplyUV(mesh, _sides[key].Type, key);
            }
        }

        private void ApplyUV(Mesh mesh, int spriteNbr, SideType side)
        {
            var rect = sprites[spriteNbr];
            var uvs = mesh.uv;
            switch (side)
            {
                case SideType.Front:
                    uvs[0] = CreateUV(rect.x, rect.yMax);
                    uvs[1] = CreateUV(rect.xMax, rect.yMax);
                    uvs[2] = CreateUV(rect.x, rect.y);
                    uvs[3] = CreateUV(rect.xMax, rect.y);
                    break;

                case SideType.Back:
                    uvs[10] = CreateUV(rect.xMax, rect.y);
                    uvs[11] = CreateUV(rect.x, rect.y);
                    uvs[6] = CreateUV(rect.xMax, rect.yMax);
                    uvs[7] = CreateUV(rect.x, rect.yMax);
                    break;

                case SideType.Left:
                    uvs[20] = CreateUV(rect.x, rect.yMax);
                    uvs[22] = CreateUV(rect.xMax, rect.yMax);
                    uvs[23] = CreateUV(rect.x, rect.y);
                    uvs[21] = CreateUV(rect.xMax, rect.y);
                    break;

                case SideType.Right:
                    uvs[16] = CreateUV(rect.x, rect.yMax);
                    uvs[18] = CreateUV(rect.xMax, rect.yMax);
                    uvs[19] = CreateUV(rect.x, rect.y);
                    uvs[17] = CreateUV(rect.xMax, rect.y);
                    break;

                case SideType.Top:
                    uvs[8] = CreateUV(rect.x, rect.y);
                    uvs[9] = CreateUV(rect.xMax, rect.y);
                    uvs[4] = CreateUV(rect.x, rect.yMax);
                    uvs[5] = CreateUV(rect.xMax, rect.yMax);
                    break;

                case SideType.Bottom:
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
            return new Vector2(x / TextureSize, (TextureSize - y) / TextureSize);
        }

        private int GetRandomSprite()
        {

            return (int)(UnityEngine.Random.value * NumberOfSymbols + 1);
        }
    }
}
                