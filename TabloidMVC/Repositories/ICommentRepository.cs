using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAllComments();

    }
}
