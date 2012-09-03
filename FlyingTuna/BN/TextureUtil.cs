using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingTuna.BN
{
    public static class TextureUtil
    {
        public static Texture2D ErrorTexture;

        // Returns the default texture to be used when one isn't found
        private static Texture2D GetErrorTexture(GraphicsDevice graphicsDevice)
        {
            if(ErrorTexture != null)
            {
                return ErrorTexture;
            }

            const int width = 32;
            const int height = 32;

            // Colors for the texture
            Color
                topLeft = Color.DarkViolet,
                topRight = Color.Teal,
                bottomLeft = Color.Teal,
                bottomRight = Color.DarkViolet;

            ErrorTexture = new Texture2D(graphicsDevice, width, height);
            var c = new Color[width * height];

            // generate a texture with 4 squares
            for (int x = 0; x < width / 2; x++ )
            {
                for(int y = 0 ; y < height / 2; y++)
                {
                    // XL = Left hand side coordinate
                    // XR = Right hand side coordinate
                    // YB = Bottom Coordinate
                    var xl = x * height;
                    var xr = (x + (width / 2)) * height;
                    var yb = y + (height / 2);

                    c[xl + y] = topLeft;
                    c[xr + y] = topRight;
                    c[xl + yb] = bottomLeft;
                    c[xr + yb] = bottomRight;
                }
            }

            ErrorTexture.SetData(c);

            return ErrorTexture;
        }

        public static Texture2D GetTexture(string res, GraphicsDevice graphicsDevice)
        {
            if (File.Exists(res))
            {
                return Texture2D.FromStream(graphicsDevice, new FileStream(res, FileMode.Open, FileAccess.Read, FileShare.Read));
            }

            Console.WriteLine("Texture not found: " + Path.GetFullPath(res));

            return GetErrorTexture(graphicsDevice);
        }
    }
}
