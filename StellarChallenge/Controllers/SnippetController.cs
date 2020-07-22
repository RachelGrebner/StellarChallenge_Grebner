using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StellarChallenge.Models;

namespace StellarChallenge.Controllers
{
	[ApiController]
    public class SnippetController : ControllerBase
    {
	    private int secondsToIncreaseSnippetExpiry = 30;

	    [HttpGet]
        [Route("snippets")]
        public IActionResult GetAllSnippets()
        {
	        if (DataStore.DataStore.Instance.Snippets.Any()) return Ok(DataStore.DataStore.Instance.Snippets.Where(s => !SnippetHasExpired(s.Expires_At)));

	        return Ok();
        }

        [HttpGet]
        [Route("snippets/{name}", Name = "GetSnippetByName")]
        public IActionResult GetSnippetByName(string name)
        {
	        var snippet = DataStore.DataStore.Instance.Snippets.Find(s => s.Name == name);

	        if (snippet == null) return NotFound($"No Snippet Was Found for {name}");
	        else if (SnippetHasExpired(snippet.Expires_At)) return NotFound($"This Snippet has Expired: {name}");

	        UpdateSnippetExpiry(snippet, secondsToIncreaseSnippetExpiry);

	        return Ok(snippet);
        }

        [HttpPost]
        [Route("snippets")]
        public IActionResult CreateSnippet(CreateSnippet createSnippet)
        {
            var snippet = new SnippetModel()
            {
                Name = createSnippet.Name,
                Expires_At = CalculateExpiresAt(createSnippet.Expires_In),
                Snippet = createSnippet.Snippet
            };

            DataStore.DataStore.Instance.Snippets.Add(snippet);

            //having trouble here when running via postman, getting the correct route for the newly created resource
            //it does create the resource correctly though

	        //return Created(new Uri($"{Request.Path}/{createSnippet.Name}"), snippet);
	        return CreatedAtRoute("GetSnippetByName", snippet);
        }

        [HttpPut]
        [Route("snippets/{name}/like")]
        public IActionResult LikeSnippet(string snippetName)
        {
	        var snippet = DataStore.DataStore.Instance.Snippets.Find(s => s.Name == snippetName);

	        if (snippet == null) return NotFound($"No Snippet Was Found for {snippetName}");
	        else if (SnippetHasExpired(snippet.Expires_At)) return NotFound($"This Snippet has Expired: {snippetName}");

	        snippet.Likes++;
            UpdateSnippetExpiry(snippet, secondsToIncreaseSnippetExpiry);

            return Ok(snippet);
        }

        private DateTime CalculateExpiresAt(int secondsUntilExpire)
        {
            return DateTime.Now.AddSeconds(secondsUntilExpire);
        }

        private bool SnippetHasExpired(DateTime snippetExpiry)
        {
	        return DateTime.Now > snippetExpiry;
        }

        private void UpdateSnippetExpiry(SnippetModel snippet, int secondsToIncreaseBy)
        {
            //since the snippet is a reference type, this should be enough
	        var newExpiry = snippet.Expires_At.AddSeconds(secondsToIncreaseBy);
	        snippet.Expires_At = newExpiry;
        }
    }
}
