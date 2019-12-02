[TestClass]
public class Engine_Hash_Test
{
    [TestMethod]
    public void testHashFromString()
    {
    	Hash_Return hash_return;
      	Hash hash_engine = new Hash();

      	hash_return = hash_engine.getHash("test");

      	Assert.AreEqual(hash_return.hash, "$21%");
    }

    [TestMethod]
    public void testHashFromAnyError()
    {
    	Hash_Return hash_return;
      	Hash hash_engine = new Hash();

      	hash_return = hash_engine.getHashFromAnyType("test");

      	Assert.AreEqual(hash_return.isRight, false);
    }
}