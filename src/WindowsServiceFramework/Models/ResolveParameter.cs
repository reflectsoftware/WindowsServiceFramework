using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceFramework.Models
{
    public class ResolveParameter
    {
        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        /// <value>
        /// The type of the parameter.
        /// </value>
        public Type ParameterType { get; private set; }

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        public object Parameter { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveParameter"/> class.
        /// </summary>
        /// <param name="parameterType">Type of the parameter.</param>
        /// <param name="parameter">The parameter.</param>
        public ResolveParameter(Type parameterType, object parameter)
        {
            ParameterType = parameterType;
            Parameter = parameter;
        }
    }

    public class ResolveParameter<T> : ResolveParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveParameter{T}"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public ResolveParameter(object parameter) : base(typeof(T), parameter)
        {
        }
    }
}
