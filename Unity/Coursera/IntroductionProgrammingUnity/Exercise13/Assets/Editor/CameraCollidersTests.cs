using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class CameraCollidersTests 
{
    [Test]
	public void Constructor_SetsAllColliders() 
    {
	    var parent = new GameObject().transform;

        var subject = new CameraColliders(parent);

        subject.Top.transform.name.Should().Be("TopCollider");
        (subject.Top.transform.parent as Component).Should().Be(parent);
        subject.Bottom.transform.name.Should().Be("BottomCollider");
        (subject.Bottom.transform.parent as Component).Should().Be(parent);
        subject.Right.transform.name.Should().Be("RightCollider");
        (subject.Right.transform.parent as Component).Should().Be(parent);
        subject.Bottom.transform.name.Should().Be("BottomCollider");
        (subject.Bottom.transform.parent as Component).Should().Be(parent);
	}
}
