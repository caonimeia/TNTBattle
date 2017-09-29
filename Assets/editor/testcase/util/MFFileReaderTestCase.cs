using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;


public class TestClass {
    public enum Te {
        a, b, c
    }


    public int id;
    public string name;
    public float rate;
    public Te te;
}

public class TestLanguage {
    public int ID;
    public string LocID;
    public string SimpleCN;
    public string TranditionalCN;
    public string English;
}


[TestFixture]
public class MFFileReaderTestCase {

    [Test]
    public void TestReadTabFile() {
        //for (int i = 1; i < 1000; i++) {
            List<TestClass> list = MFTabFileReader.LoadTabFile<TestClass>("Assets/tabfile/test.tab");
            TestClass tc = list[1];
            Assert.AreEqual(tc.id, 2);
            Assert.AreEqual(tc.name, "发啊");
            Assert.AreEqual(tc.rate, 0.01f);
            Assert.AreEqual(tc.te, TestClass.Te.b);
        //}
        //List<TestLanguage> list_1 = MFTabFileReader.LoadTabFile<TestLanguage>("Assets/tabfile/Language.tab");
    }
}
