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
        BigDouble p0 = new BigDouble("1e100000000000000000000000001");
        BigDouble p1 = new BigDouble("1e100000000000000000000000002");
        Assert.IsTrue(p0 < p1); 
    }
    [Test]
    public void Div()
    {
        BigDouble p0 = new BigDouble("1e1000000");
        BigDouble p1 = new BigDouble("1e500000");
        BigDouble p2 = new BigDouble("1e"+(1000000-500000).ToString());
        Assert.IsTrue((p0 / p1) == p2);
    }

    [Test]
    public void PM()
    {
        BigDouble p1 = new BigDouble("1e10000");
        BigDouble p2 = new BigDouble("1e10000");
        BigDouble p3= new BigDouble("1e10000");
        BigDouble p4 = new BigDouble("1e10000");



        Assert.IsTrue((p1 + p2) == (p3 + p4));
        Assert.IsTrue((p1 + p2) != (p3 - p4));
        Assert.IsTrue((p1 - p2) == (p3 - p4));

    }
    [Test]
    public void Mul()
    {
        BigDouble res = 1e110;
        BigDouble p1 = 1e100;
        BigDouble p2 = 1e10;
        Assert.IsTrue((p1 * p2) == res);
    }
    [Test]
    public void TestEquals()
    {
        {
            BigDouble a = new BigDouble("1.0A");
            BigDouble b = new BigDouble("1000");
            BigDouble c = new BigDouble("1e3");
            Assert.IsTrue(a == b && b == c); 
        }  
    } 
}