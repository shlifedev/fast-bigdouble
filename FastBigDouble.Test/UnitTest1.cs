namespace LD.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
 
    [Test]
    public void TestEquals()
    {
        {
            FastBigDouble a = new FastBigDouble("1.0A");
            FastBigDouble b = new FastBigDouble("1000");
            FastBigDouble c = new FastBigDouble("1e3");
            Assert.IsTrue(a == b && b == c);
            a.
        }  
    }
    [Test]
    public void Performance()
    {
        {
            FastBigDouble a = new FastBigDouble("1.0A");
            FastBigDouble b = new FastBigDouble("1000");
            FastBigDouble c = new FastBigDouble("1e3");
            Assert.IsTrue(a == b && b == c);
        }  
    }
}