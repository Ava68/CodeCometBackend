using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodePulse.API.Models.DTO;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using System;
using CodePulse.API.Repositories.Implementation;

namespace CodePulse.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepositiry;
        private readonly ICategoryRepository categoryRepository;
        public BlogPostsController(IBlogPostRepository blogPostRepositiry, ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }
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
                Author = blogPost.Author,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }
        // Get : {apibaseurl}/api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await blogPostRepositiry.GetAllAsync();
            // convert Domain model to dto
            var response = new List<BlogPostDto>();

            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    id = blogPost.id,
                    Title = blogPost.Title,
                    shortDescription = blogPost.shortDescription,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    IsVisible = blogPost.IsVisible,
                    Author = blogPost.Author,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }
            return Ok(response);
        }
        // Get: {apiBaseUrl}/api/blogposts/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            // Get the Blogpost from Repository
            var blogPost = await blogPostRepositiry.GetByIdAsync(id);
            if (blogPost is null)
                return NotFound();
            // Convert Domain model to dto
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
                Author = blogPost.Author,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        // Get :{apibaseurl}/api/blogposts/{urlHandle}
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
            // get blogpost details from repository
            var blogPost = await blogPostRepositiry.GetByUrlHandleAsync(urlHandle);
            if (blogPost is null)
                return NotFound();
            // Convert Domain model to dto
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
                Author = blogPost.Author,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        // Put : {apibaseurl}/api/blogposts/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, UpdateBlogPostRequestDto request)
        {
            // Convert Dto to Domain Model

            var blogPost = new BlogPost
            {
                id = id,
                Author = request.Author,
                Title = request.Title,
                shortDescription = request.shortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()
            };
            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory != null)
                    blogPost.Categories.Add(existingCategory);
            }
            // call Repository to Update Blogpost domain model
            var updatedBlogPost = await blogPostRepositiry.UpdateAsync(blogPost);
            if (updatedBlogPost == null)
                return NotFound();

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
                Author = blogPost.Author,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // Delete: {apibaseurl}/api/blogposts/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var deletedBlogPost = await blogPostRepositiry.DeleteAsync(id);
            if (deletedBlogPost == null)
            {
                return NotFound();
            }
            // convert Domain model to dto
            var response = new BlogPostDto
            {
                id = deletedBlogPost.id,
                Title = deletedBlogPost.Title,
                shortDescription = deletedBlogPost.shortDescription,
                Content = deletedBlogPost.Content,
                FeaturedImageUrl = deletedBlogPost.FeaturedImageUrl,
                UrlHandle = deletedBlogPost.UrlHandle,
                PublishedDate = deletedBlogPost.PublishedDate,
                IsVisible = deletedBlogPost.IsVisible,
                Author = deletedBlogPost.Author
            };
            return Ok(response);
        }
    }
}
