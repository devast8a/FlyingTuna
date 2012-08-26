using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingTuna.Graphics
{
    public static class Primitives
    {
        private static Texture2D _pixel;

        private static void SetupPixel(SpriteBatch spriteBatch)
        {
            if(_pixel == null)
            {
                _pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pixel.SetData(new Color[] { new Color(255, 255, 255) });
            }
        }

        static public Texture2D Pixel(SpriteBatch spriteBatch)
        {
            SetupPixel(spriteBatch);

            return _pixel;
        }

        static public void DrawRectangle(this SpriteBatch spriteBatch, Rectangle r, Color color, float rotation = 0, float depth = 1.0f)
        {
            SetupPixel(spriteBatch);
            spriteBatch.Draw(_pixel, r, new Rectangle(0, 0, 1, 1), color, rotation, Vector2.Zero, SpriteEffects.None, depth);
        }

        static public void DrawRectangleOutline(this SpriteBatch spriteBatch, Rectangle r, Color color, int lineWidth = 1, float rotation = 0, float depth = 1.0f)
        {
            DrawRectangle(spriteBatch, new Rectangle(r.X, r.Y + lineWidth, lineWidth, r.Height - lineWidth), color, rotation, depth);
            DrawRectangle(spriteBatch, new Rectangle(r.X + r.Width - lineWidth, r.Y + lineWidth, lineWidth, r.Height - lineWidth), color, rotation, depth);
            DrawRectangle(spriteBatch, new Rectangle(r.X, r.Y, r.Width, lineWidth), color, rotation, depth);
            DrawRectangle(spriteBatch, new Rectangle(r.X, r.Y + r.Height, r.Width, lineWidth), color, rotation, depth);
        }
        
        static public void DrawValueBar(this SpriteBatch spriteBatch, Rectangle r, float value, Color col1, Color col2, float depth = 1.0f)
        {
            var l = (int)Math.Ceiling(r.Width * value);

            var r1 = new Rectangle(r.X, r.Y, l, r.Height);
            var r2 = new Rectangle(r.X + l, r.Y, r.Width - l, r.Height);
            DrawRectangle(spriteBatch, r1, col1, 0, depth);
            DrawRectangle(spriteBatch, r2, col2, 0, depth);
        }
    }
}
