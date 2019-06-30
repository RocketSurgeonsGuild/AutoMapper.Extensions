using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rocket.Surgery.Extensions.AutoMapper.Builders
{
    /// <summary>
    /// Class AutoMapperProfileContainer.
    /// </summary>
    class AutoMapperProfileContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperProfileContainer"/> class.
        /// </summary>
        /// <param name="profiles">The profiles.</param>
        /// <exception cref="ArgumentNullException">profiles</exception>
        public AutoMapperProfileContainer(IEnumerable<TypeInfo> profiles)
        {
            Profiles = profiles ?? throw new ArgumentNullException(nameof(profiles));
        }

        /// <summary>
        /// Gets the profiles.
        /// </summary>
        /// <value>The profiles.</value>
        public IEnumerable<TypeInfo> Profiles { get; }
    }
}
