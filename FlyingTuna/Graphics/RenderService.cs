using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingTuna.Graphics
{
    public class RenderService
    {
        public readonly GraphicsDevice Device;
        public readonly SpriteBatch SpriteBatch;

        public RenderService(SpriteBatch spriteBatch, GraphicsDevice device)
        {
            SpriteBatch=spriteBatch;
            Device=device;
        }
    }
}
