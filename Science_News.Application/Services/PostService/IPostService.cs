using Science_News.Application.Models.DTOs.Post;
using Science_News.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Science_News.Application.Services.PostService
{
    public interface IPostService
    {

        Task Create(PostCreateDTO model);
        Task Delete(int id);
        Task Update(PostUpdateDTO model);

        Task<PostUpdateDTO> GetById(int id);

        Task<PostDetailsVM> PostDetails(int id);
        Task<PostCreateDTO> CreatePost();

        Task<List<PostVM>> GetPosts();

        Task<List<GetPostsVM>> GetPostsForMembers();



    }
}
