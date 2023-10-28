using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Used to clone any Serializable-class object (Create an object copy)
/// </summary>
public static class ObjectCopier
{
    
    /// <summary>
    /// Perform a deep copy of the object via serialization.
    /// </summary>
    public static T Clone<T>(T source)
    {
        if (!typeof(T).IsSerializable)
        {
            throw new ArgumentException($"The type {nameof(T)}must be serializable");   // only serializable types
        }

        if (ReferenceEquals(source, null))
        {
            return default;     // returns default value of T type
        }
        
        using Stream stream = new MemoryStream();
        IFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, source);
        stream.Seek(0, SeekOrigin.Begin);
        return (T)formatter.Deserialize(stream);
    }
}
