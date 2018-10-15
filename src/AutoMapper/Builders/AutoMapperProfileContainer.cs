using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rocket.Surgery.Extensions.AutoMapper.Builders
{
    class AutoMapperProfileContainer
    {
        public AutoMapperProfileContainer(IEnumerable<TypeInfo> profiles)
        {
            Profiles = profiles ?? throw new ArgumentNullException(nameof(profiles));
        }

        public IEnumerable<TypeInfo> Profiles { get; }
    }
}
