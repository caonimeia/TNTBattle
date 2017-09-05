using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class MFFileReaderTestCase {

    [Test]
    public void TestReadTabFile()
    {
        MFFileReader.LoadTabFile("Assets/tabfile/test.tab", "2");
    }
}
