using System;
using System.Text;
using System.Drawing;

namespace BoardSoupEngine.Assets
{
    class TextAsset : Asset
    {
        private String text;
        
        public TextAsset()
        {
            this.setText("");
        }

        public TextAsset(String argText)
        {
            this.setText(argText);
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
            throw new NotImplementedException();
        }
    }
}
