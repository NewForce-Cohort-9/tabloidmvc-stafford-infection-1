using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        //List<Comment> GetAllComments();
        List<Comment> GetCommentsByPostId(int postId);
        void Add(Comment comment);
        Comment GetCommentById(int postId); //for DELETE
        void Delete(int commentId);
    }
}
