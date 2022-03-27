using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SecondLab;

namespace CipherTesting
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void AtbashCodingTest()
        {
            const String toEncode = "If he had anything confidential to say he wrote it in cipher";
            const String expectations = "Ru sv szw zmbgsrmt xlmurwvmgrzo gl hzb sv dilgv rg rm xrksvi";

            Atbash atbash = new Atbash();
            String result = atbash.Encode(toEncode);

            Assert.AreEqual(result, expectations);
        }
        [TestMethod]
        public void AtbashDecodingTest()
        {
            const String toDecode = "Olivn rkhfn wloli hrg znvg";
            const String expectations = "Lorem ipsum dolor sit amet";

            Atbash atbash = new Atbash();
            String result = atbash.Decode(toDecode);

            Assert.AreEqual(result, expectations);
        }
        [TestMethod]
        public void AffineCodingTest()
        {
            const String toEncode = "Ut eget diam convallis tincidunt";
            const int keyA = 7, keyB = 2;
            const String expectations = "Mf esef xgci qwptcbbgy fgpqgxmpf";

            Affine affine = new Affine();
            String result = affine.Encode(toEncode, keyA, keyB);

            Assert.AreEqual(result, expectations);
        }
        [TestMethod]
        public void AffineDecodingTest()
        {
            const String toDecode = "Sv tvz gvqh uv mdex tvzk mxdku rquxkgrqbxs";
            const int keyA = 5, keyB = 3;
            const String expectations = "Do you long to have your heart interlinked";

            Affine affine = new Affine();
            String result = affine.Decode(toDecode, keyA, keyB);

            Assert.AreEqual(result, expectations);
        }
    }
}