using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingTuna.Graphics
{
    public class Image
    {
        public readonly Texture2D Texture;
        public readonly Rectangle Source;
        public Color Color = Color.White;

        public Image(Texture2D texture)
        {
            Texture = texture;
            Source = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        public Image(Texture2D texture, Rectangle source)
        {
            Texture = texture;
            Source = source;
        }

        public Image Slice(int width, int height)
        {
            return Slice(new Rectangle(0, 0, width, height));
        }

        public Image Slice(int x, int y, int width, int height)
        {
            return Slice(new Rectangle(x, y, width, height));
        }

        public Image Slice(Rectangle rectangle)
        {
            return new Image(Texture, rectangle){Color = Color};
        }

        public void Draw(SpriteBatch spritebatch, Vector2 position)
        {
            spritebatch.Draw(Texture, position, Source, Color);
        }
    }
}
