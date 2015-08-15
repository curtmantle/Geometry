using System;
using System.Collections.Generic;
using System.Linq;

namespace Geometry.Model
{
    public interface IPathOutliner
    {
        IPathOutline Generate();
    }
}
