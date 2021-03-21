using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class StringParameterTest7Source
    {
        public unsafe void Test(
            int i1,
            object o1, 
            object o2, 
            object o3,
            string s1,
            string s2,
            string s3,
            string s4,
            string s5,
            string s6,
            string s7,
            double[] d1,
            double[] d2,
            double[] d3,
            double[] d4,
            byte* p1,
            byte* p2,
            byte* p3)
        {
            Requires.NotNull(o1, nameof(o1));
            Debug.Assert(o2 is not null);
            Requires.NotNull(s1, nameof(s1));
            Debug.Assert(s2 is not null);
            Requires.NotNullOrEmpty(s3, nameof(s3));
            Debug.Assert(!string.IsNullOrEmpty(s4));
            Requires.NotNullOrWhitespace(s5, nameof(s5));
            Debug.Assert(!string.IsNullOrWhiteSpace(s6));
            Requires.NotNull(d1, nameof(d1));
            Debug.Assert(d2 is not null);
            Requires.NotNullOrEmpty(d3, nameof(d3));
            Requires.NotNullPtr(p1, nameof(p1));
            Debug.Assert(p2 is not null);

        }
    }
}
