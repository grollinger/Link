﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tavis
{
    public interface ILinkExtractor
    {
            Type SupportedType { get; }
            Link GetLink(Func<string, Link> factory, object content, string relation, string anchor = null);
            IEnumerable<Link> GetLinks(Func<string,Link> factory, object content, string relation = null, string anchor = null);
    }
}
