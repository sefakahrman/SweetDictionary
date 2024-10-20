using Core.Repository;
using SweetDictionary.Models.Entities;
using SweetDictionary.Repository.Contexts;
using SweetDictionary.Repository.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetDictionary.Repository.Repositories.Concretes
{
    public class EfPostRepository : EfRepositoryBase<BaseDbContext, Post, Guid>, IPostRepository
    {
        public EfPostRepository(BaseDbContext context) : base(context)
        {
        }

        public List<Post> GetAllByAuthorId(long authorId)
        {

            // select * from posts where AuthorId = authorId
            List<Post> posts = Context.Posts.Where(x => x.AuthorId == authorId).ToList();
            return posts;        
        }

        public List<Post> GetAllByCategoryId(int categoryId)
        {
            List<Post> posts = Context.Posts.Where(x => x.CategoryId == categoryId).ToList();
            return posts;
        }

        public List<Post> GetAllByTitleContains(string text)
        {
            List<Post> posts = Context.Posts
                .Where(x => x.Title.Contains(text, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            return posts;
        }
    }
}
