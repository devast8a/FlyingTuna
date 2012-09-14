using System;

namespace FlyingTuna.Utils.Extensions.ICustomAttributeProvider
{
    public static class GetCustomAttributeExtension
    {
        public static T GetCustomAttributeOrNull<T>(this System.Reflection.ICustomAttributeProvider provider) where T : Attribute
        {
            return provider.GetCustomAttributeOrNull<T>(true);
        }

        public static T GetCustomAttributeOrNull<T>(this System.Reflection.ICustomAttributeProvider provider, bool inherit) where T : Attribute
        {
            var attributes = provider.GetCustomAttributes(typeof(T), inherit);
            
            if(attributes.Length != 1)
            {
                return null;
            }

            var attr = attributes[0];

            if(attr is T)
            {
                return (T)attr;
            }

            return null;
        }
    }
}
