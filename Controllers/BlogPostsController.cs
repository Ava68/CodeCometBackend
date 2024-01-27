using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodePulse.API.Models.DTO;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using System;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepositiry;
        public BlogPostsController(IBlogPostRepository blogPostRepositiry)
        {
            this.blogPostRepositiry = blogPostRepositiry;
        }
        // POST : {apibaseurl}/api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDto request)
        {
            // convert dto to domain model
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Title = request.Title,
                shortDescription = request.shortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                IsVisible = request.IsVisible
            };
            blogPost = await blogPostRepositiry.CreateAsync(blogPost);
            // Convert domain model to dto
            var response = new BlogPostDto
            {
                id = blogPost.id,
                Title = blogPost.Title,
                shortDescription = blogPost.shortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                IsVisible = blogPost.IsVisible,
                Author = blogPost.Author
            };

            return Ok(response);
        }
    }
}