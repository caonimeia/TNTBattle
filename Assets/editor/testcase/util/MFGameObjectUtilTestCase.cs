using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[TestFixture]
public class MFGameObjectUtilTestCase {
    [Test]
    public void TestGetTransform() {
        GameObject obj = new GameObject();
        Rigidbody r = obj.AddComponent<Rigidbody>();

        Assert.AreEqual(obj.transform, MFGameObjectUtil.GetTransform(obj));
        Assert.AreEqual(obj.transform, MFGameObjectUtil.GetTransform(obj.transform));
        Assert.AreEqual(obj.transform, MFGameObjectUtil.GetTransform(r));
        Assert.IsNull(MFGameObjectUtil.GetTransform(null));
    }

    [Test]
    public void TestFind_T() {
        // mock
        GameObject parent = new GameObject("parent");
        Rigidbody pr1 = parent.AddComponent<Rigidbody>();
        GameObject child = new GameObject("aaa");
        Rigidbody r1 = child.AddComponent<Rigidbody>();
        child.AddComponent<MeshFilter>();
        child.transform.SetParent(parent.transform);

        Rigidbody r2 = MFGameObjectUtil.Find<Rigidbody>(parent, "aaa");
        Assert.IsNotNull(r2);
        Assert.AreEqual(r1, r2);

        Rigidbody r3 = MFGameObjectUtil.Find<Rigidbody>(pr1, "aaa");
        Assert.IsNotNull(r3);
        Assert.AreEqual(r1, r3);
    }

    [Test]
    public void TestFind() {
        // mock
        GameObject parent = new GameObject("parent");
        Rigidbody pr1 = parent.AddComponent<Rigidbody>();
        GameObject child = new GameObject("aaa");
        child.transform.SetParent(parent.transform);

        GameObject o1 = MFGameObjectUtil.Find(parent, "aaa");
        Assert.IsNotNull(o1);
        Assert.AreEqual(child, o1);

        GameObject o2 = MFGameObjectUtil.Find(pr1, "aaa");
        Assert.IsNotNull(o2);
        Assert.AreEqual(child, o2);
        Assert.AreEqual(o1, o2);
    }
}
