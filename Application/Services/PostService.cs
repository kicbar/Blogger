using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public IEnumerable<PostDto> GetAllPosts()
        {
            var posts = _postRepository.GetAll();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public PostDto GetPostById(int id)
        {
            var post = _postRepository.GetById(id);
            return _mapper.Map<PostDto>(post);
        }

        public PostDto AddNewPost(CreatePostDto newPostDto)
        {
            if (string.IsNullOrEmpty(newPostDto.Title))
                throw new Exception("Post can not have an empty title.");

            var post = _mapper.Map<Post>(newPostDto);
            _postRepository.Add(post);
            return _mapper.Map<PostDto>(post); ;
        }

        public void UpdatePost(UpdatePostDto updatePostDto)
        {
            var existingPost = _postRepository.GetById(updatePostDto.Id);
            var post = _mapper.Map(updatePostDto, existingPost);
            _postRepository.Update(post);
        }

        public void DeletePost(int id)
        {
            var post = _postRepository.GetById(id);
            _postRepository.Delete(post);
        }
    }
}
