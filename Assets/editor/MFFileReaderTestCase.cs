using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class MFFileReaderTestCase {

    [Test]
    public void TestReadTabFile()
    {
        List<TestClass> list = MFTabFileReader.LoadTabFile<TestClass>("Assets/tabfile/test.tab");
        foreach (var item in list) {
            TestClass tc = item as TestClass;
            MFLog.LogObject(tc);
            MFLog.LogInfo("==================");
        }
    }
}
