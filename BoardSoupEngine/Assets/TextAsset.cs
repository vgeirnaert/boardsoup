using System;
using System.Text;
using System.Drawing;

namespace BoardSoupEngine.Assets
{
    class TextAsset : Asset
    {
        private String text;
        private Rectangle bounds;
        private String font;
        private int fontsize;
        
        public TextAsset()
        {
            font = "Arial";
            fontsize = 16;
            this.setText("");
            this.setBounds(new Rectangle());
        }

        public TextAsset(String argText)
        {
            font = "Arial";
            fontsize = 16;
            this.setText(argText);
            this.setBounds(new Rectangle());
        }

        public TextAsset(String argText, String argFont, int argSize)
        {
            font = argFont;
            fontsize = argSize;
            this.setText(argText);
            this.setBounds(new Rectangle());
        }

        public String getText()
        {
            return text;
        }

        public void setText(String argText)
        {
            text = argText;
            this.setName(text);
        }

        public override Rectangle getBounds()
        {
            return bounds;
        }

        public void setBounds(Rectangle argBounds)
        {
            bounds = argBounds;
        }

        public String getFontName()
        {
            return font;
        }

        public int getFontSize()
        {
            return fontsize;
        }
    }
}
