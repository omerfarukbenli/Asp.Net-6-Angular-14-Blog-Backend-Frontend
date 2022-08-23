using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheDevBlog.API.Data;
using TheDevBlog.API.Models.DTO;
using TheDevBlog.API.Models.Entites;

namespace TheDevBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : Controller
    {
        private readonly TheDevBlogDbContext _dbContext;

        public PostsController(TheDevBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPosts()
        {
            var posts = await _dbContext.Posts.ToListAsync();
            return Ok(posts);
        }
        [HttpGet]
        [ActionName("GetPostById")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
           var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if(post != null)
            {
                return Ok(post);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {
            var post = new Post()
            {
                Title = addPostRequest.Title,
                Content = addPostRequest.Content,
                Author = addPostRequest.Author,
                FeaturedImageUrl = addPostRequest.FeaturedImageUrl,
                PublishDate = addPostRequest.PublishDate,
                UpdatedDate = addPostRequest.UpdatedDate,
                Summary = addPostRequest.Summary,
                UrlHandle = addPostRequest.UrlHandle,   
                Visible = addPostRequest.Visible,
            };
            post.Id = Guid.NewGuid();
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);

        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest updatePostRequest)
        {
            
             var existingPost = await _dbContext.Posts.FindAsync(id);    
            if (existingPost != null)
            {
                existingPost.Title = updatePostRequest.Title;
                existingPost.Content = updatePostRequest.Content;
                existingPost.Author = updatePostRequest.Author;
                existingPost.FeaturedImageUrl = updatePostRequest.FeaturedImageUrl;
                existingPost.PublishDate = updatePostRequest.PublishDate;
                existingPost.UpdatedDate = updatePostRequest.UpdatedDate;
                existingPost.Summary = updatePostRequest.Summary;
                existingPost.UrlHandle = updatePostRequest.UrlHandle;
                existingPost.Visible = updatePostRequest.Visible;
                await _dbContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var existingPost   = await _dbContext.Posts.FindAsync(id);
            if(existingPost != null)
            {
                _dbContext.Remove(existingPost);
                await _dbContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound();
        }

    }
}
