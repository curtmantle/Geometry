using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry.Elements
{
    public interface IElementRenderer
    {
        void Render();
    }

    public class RenderManager
    {
        private Dictionary<Type, IElementRenderer> renderDictionary = new Dictionary<Type, IElementRenderer>();


    }
}
