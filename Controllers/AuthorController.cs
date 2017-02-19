using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DotnetCoreTrademeStats.Controllers {

	[Route("api/[controller]")]
	public class AuthorsController : Controller {
		private readonly ArticleContext _dbContext;

		public AuthorsController(ArticleContext dbContext) {
			_dbContext = dbContext;
		}

		// GET: api/authors
		public IEnumerable<Author> Get(){
			return _dbContext.Authors.ToList();
		}

		// GET: api/authors/5
		[HttpGet("{id}")]
		public Author Get(int id){
			return _dbContext.Authors.FirstOrDefault(a => a.Id == id);
		}

		[HttpPost]
		public IActionResult Post([FromBody]Author value){
			_dbContext.Authors.Add(value);
			_dbContext.SaveChanges();
			return StatusCode(201, value);
		}
	}
}