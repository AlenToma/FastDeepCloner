using System;
using static FastDeepCloner.Extensions;

namespace FastDeepCloner
{
    public class FastDeepClonerSettings
    {
        public FieldType FieldType { get; set; }

        /// <summary>
        /// override Activator CreateInstance and use your own method
        /// </summary>
        public CreateInstance OnCreateInstance { get; set; }


        public FastDeepClonerSettings()
        {
            OnCreateInstance = new CreateInstance((Type type) =>
            {
                return type.Creator();
            });
        }

    }
}
