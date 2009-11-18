using System;
using System.Text;
using System.Drawing;

namespace BoardSoupEngine.Assets
{
    class TextAsset : Asset
    {
        private String text;
        private Rectangle bounds;
        
        public TextAsset()
        {
            this.setText("");
            this.setBounds(new Rectangle());
        }

        public TextAsset(String argText)
        {
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
            return "Arial";
        }

        public int getFontSize()
        {
            return 16;
        }
    }
}
