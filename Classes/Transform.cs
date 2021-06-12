using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dino.Classes
{
    public class Transform
    {
        public PointF Position;
        public Size Size;

        public Transform(PointF pos,Size size)
        {
            this.Position = pos;
            this.Size = size;
        }
    }
}
