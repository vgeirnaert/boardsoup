using System;
using BoardSoupEngine.Utilities;
using BoardSoupEngine.Assets;

namespace BoardSoupEngine.Interface
{
    public abstract class ImageActor : ActorObject
    {
        public ImageActor(int x, int y, String imageName) : base(x, y, imageName, AssetType.IMAGE)
        {
        }

        public virtual void setImage(String filename)
        {
            if (dispatcher != null)
                actor.loadAsset(filename, dispatcher, new AssetDetails(AssetType.IMAGE));
            else
                Logger.log("ActorObject: Warning - no event dispatcher, unable to set actor image.", LEVEL.WARNING);
        }
    }
}
