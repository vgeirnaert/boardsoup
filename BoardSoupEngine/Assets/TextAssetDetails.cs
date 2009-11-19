using System;
using System.Drawing;

namespace BoardSoupEngine.Assets
{
    class TextAssetDetails : AssetDetails
    {
        public String fontName;
        public int fontSize;
        public Color fontColor;

        public TextAssetDetails(AssetType myType, String argFont, int argFontSize, Color argFontColor) : base(myType)
        {
            fontName = argFont;
            fontSize = argFontSize;
            fontColor = argFontColor;
        }
    }
}
