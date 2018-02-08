using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishFourm.Application.Coments.DTO;
using FishFourm.Core.Repository;
using AutoMapper;
using FishFourm.Core.Entity;
using Abp.Application.Services;

namespace FishFourm.Application.Coments
{
    public class CommentAppService : ApplicationService,ICommentAppService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public CommentAppService(ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public async Task<CommentOutput> AddComment(CommentInput commentInput)
        {
            var comment = new Comment(commentInput.PostId, commentInput.AuthorId, commentInput.Content);
            comment = await _commentRepository.AddComment(comment);

            var user = await _userRepository.FirstOrDefaultAsync(comment.AuthorId);

            var output = Mapper.Map<CommentOutput>(comment);
            output.AuthorName = user.UserName;

            return output;
        }

        public async Task<IEnumerable<CommentOutput>> GetComments(Guid postId)
        {
       
            var comments = await _commentRepository.GetComments(postId);
            var users = _userRepository.GetAll();

            var commentsOutput = comments.Join(users, a => a.AuthorId, b => b.Id,
                (a, b) => new CommentOutput
                {
                    Id = a.Id,
                    PostId = a.PostId,
                    AuthorName = b.UserName,
                    Content = a.Content,
                    CreateTime = a.CreateTime
                }).ToList();

            return commentsOutput;
        }
    }
}
