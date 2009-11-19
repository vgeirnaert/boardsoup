using System;
using BoardSoupEngine.Assets;
namespace BoardSoupEngine.Interface
{
    public abstract class ShapeActor : ActorObject
    {
        public ShapeActor(int x, int y, String imageName) : base(x, y, imageName, AssetType.SHAPE)
        {
        }
    }
}
