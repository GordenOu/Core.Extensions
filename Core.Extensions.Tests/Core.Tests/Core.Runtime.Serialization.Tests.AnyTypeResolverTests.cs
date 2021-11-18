using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Core.Tests;

[DataContract]
public class Class3<T>
{
    [DataMember]
    public ReadOnlyCollection<T> List { get; set; }
}

[DataContract]
public class Class4<T> : Class3<T>
{
    [DataMember]
    public Class3<T> Value { get; set; }
}
