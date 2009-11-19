using System;
using BoardSoupEngine.Assets;
using BoardSoupEngine.Utilities;
using System.Drawing;

namespace BoardSoupEngine.Interface
{
    public abstract class TextActor : ActorObject
    {
        private String font;
        private int fontsize;
        private Color fontColor;
        private String text;

        public TextActor(int x, int y, String imageName) : base(x, y, imageName, AssetType.TEXT)
        {
            font = "Arial";
            fontsize = 16;
            fontColor = Color.White;
            text = imageName;
        }

        public void setText(String argText)
        {
            if (!text.Equals(argText))
            {
                text = argText;
                update();
            }
        }

        public void setFont(String fontname)
        {
            if (!this.font.Equals(fontname))
            {
                this.font = fontname;
                update();
            }
        }

        public void setFontSize(int size)
        {
            if (!(this.fontsize == size))
            {
                this.fontsize = size;
                update();
            }
        }

        public void setFontColor(Color color)
        {
            if (!this.fontColor.Equals(color))
            {
                this.fontColor = color;
                update();
            }
        }

        private void update()
        {
            // todo: figure out why this causes the 'object to dissapear' if a new font, size or colour is selected
            if (dispatcher != null)
            {
                TextAssetDetails tad = new TextAssetDetails(AssetType.TEXT, this.font, this.fontsize, this.fontColor);
                actor.loadAsset(text, dispatcher, tad);
            }
            else
                Logger.log("ActorObject: Warning - no event dispatcher, unable to set actor image.", LEVEL.WARNING);
        }
    }
}
