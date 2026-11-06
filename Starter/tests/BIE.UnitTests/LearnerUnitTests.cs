using BIE.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIE.UnitTests;

[TestClass]
public class LearnerUnitTests
{
    [TestMethod]
    public void TouristAt30DaysIsApproved()
    {
        var entry = new Entry("Sam Test", "Orion", "Tourismus", 30, false, false, false, true);
        // TODO: Act und Assert gemäss R3 ergänzen.
        
         var result = EntryPolicy.Evaluate(entry);
        
        Assert.AreEqual("approved", result.Status);
        
     }
}
