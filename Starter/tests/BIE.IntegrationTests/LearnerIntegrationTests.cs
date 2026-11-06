using System.Net;
using System.Net.Http.Json;
using BIE.Core;
using Microsoft.ApplicationInsights.Metrics.Extensibility;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bie.IntegrationTests;

[TestClass]
public class LearnerIntegrationTests
{
    [TestMethod]
    public async Task TouristAt30DaysIsSavedAsApproved()
    {
        using var app = new BieFactory();
        using var client = app.CreateClient();
        var entry = new Entry("Sam Test", "Orion", "Tourismus", 30, false, false, false, true);
        var response = await client.PostAsJsonAsync("/api/cases", entry);
        var records = await client.GetFromJsonAsync<List<Case>>("/api/cases");
        
        //HttpClient 201
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        
        //Liste lesen
        var list = await client.GetFromJsonAsync<List<Case>>("/api/cases");

        //Liste nicht null
        Assert.IsNotNull(list);

        //Genau ein Eintrag
        Assert.AreEqual(1, list.Count);

        var item = list[0];

        //Applicant gleiche entry
        Assert.AreEqual(entry, item.Applicant);

        //ID grösser 0
        Assert.IsTrue(item.Id > 0);

        //Status approved
        Assert.AreEqual("approved", item.Decision.Status);

        
    }
}
