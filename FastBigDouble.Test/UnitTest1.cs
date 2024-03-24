namespace LD.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Less()
    {
        FastBigDouble p0 = new FastBigDouble("1e100000000000000000000000001");
        FastBigDouble p1 = new FastBigDouble("1e100000000000000000000000002");
        Assert.IsTrue(p0 < p1); 
    }
    [Test]
    public void Div()
    {
        FastBigDouble p0 = new FastBigDouble("1e1000000");
        FastBigDouble p1 = new FastBigDouble("1e500000");
        FastBigDouble p2 = new FastBigDouble("1e"+(1000000-500000).ToString());
        Assert.IsTrue((p0 / p1) == p2);
    }`

    [Test]
    public void PM()
    {
        FastBigDouble p1 = new FastBigDouble("1e10000");
        FastBigDouble p2 = new FastBigDouble("1e10000");
        FastBigDouble p3= new FastBigDouble("1e10000");
        FastBigDouble p4 = new FastBigDouble("1e10000");



        Assert.IsTrue((p1 + p2) == (p3 + p4));
        Assert.IsTrue((p1 + p2) != (p3 - p4));
        Assert.IsTrue((p1 - p2) == (p3 - p4));

    }
    [Test]
    public void Mul()
    {
        FastBigDouble res = 1e110;
        FastBigDouble p1 = 1e100;
        FastBigDouble p2 = 1e10;
        Assert.IsTrue((p1 * p2) == res);
    }
    [Test]
    public void TestEquals()
    {
        {
            FastBigDouble a = new FastBigDouble("1.0A");
            FastBigDouble b = new FastBigDouble("1000");
            FastBigDouble c = new FastBigDouble("1e3");
            Assert.IsTrue(a == b && b == c); 
        }  
    } 
}