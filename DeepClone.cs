namespace FastDeepCloner
{
    public static class DeepCloner
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="fieldType">Clone Method</param>
        /// <returns></returns>
        public static object Clone(object objectToBeCloned, FieldType fieldType = FieldType.PropertyInfo)
        {
            return new ClonerShared().Clone(objectToBeCloned, fieldType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToBeCloned">Desire object to cloned</param>
        /// <param name="fieldType">Clone Method</param>
        /// <returns></returns>
        public static T Clone<T>(T objectToBeCloned, FieldType fieldType = FieldType.PropertyInfo) where T : class
        {
            return (T)new ClonerShared().Clone(objectToBeCloned, fieldType);
        }
    }
}
