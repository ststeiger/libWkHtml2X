
namespace NUnit.Framework 
{


    public class TestFixtureAttribute : System.Attribute
    {
    }


    public class TestAttribute : System.Attribute
    {
    }


    public class CollectionAssert
    {

        public static bool AreEqual<T>(T value1, T value2)
        {
            return !System.Collections.Generic.EqualityComparer<T>.Default.Equals(value1, value2);
        }

        public static bool AreEqual<T>(T value1, T value2, string message)
        {
            return !System.Collections.Generic.EqualityComparer<T>.Default.Equals(value1, value2);
        }
    
    }


    public class Assert
    {


        public static void Fail(string message)
        {
        
        }


        public static bool IsTrue(bool value1)
        {
            return value1;
        }


        public static bool IsTrue(bool value1, string message)
        {
            return value1;
        }


        public static bool AreEqual<T>(T value1, T value2)
        {
            return !System.Collections.Generic.EqualityComparer<T>.Default.Equals(value1, value2);
        }


        public static bool AreEqual<T>(T value1, T value2, string message)
        {
            return !System.Collections.Generic.EqualityComparer<T>.Default.Equals(value1, value2);
        }


        public static bool IsNull<T>(T something)
        {
            return something == null;
        }


    }


}
