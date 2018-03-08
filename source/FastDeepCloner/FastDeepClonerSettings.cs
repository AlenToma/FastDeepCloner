using System;
using static FastDeepCloner.Extensions;

namespace FastDeepCloner
{
    /// <summary>
    /// FastDeepClonerSettings
    /// </summary>
    public class FastDeepClonerSettings
    {
        /// <summary>
        /// Field type
        /// </summary>
        public FieldType FieldType { get; set; }

        /// <summary>
        ///  Clone Level
        /// </summary>
        public CloneLevel CloneLevel { get; set; }

        /// <summary>
        /// override Activator CreateInstance and use your own method
        /// </summary>
        public CreateInstance OnCreateInstance { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FastDeepClonerSettings()
        {
            OnCreateInstance = new CreateInstance((Type type) =>
            {
                return type.Creator();
            });
        }

    }
}
