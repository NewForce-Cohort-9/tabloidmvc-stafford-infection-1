﻿using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Delete(int postId);
        void Update(Post post);
        List<Post> GetAllPublishedPosts();
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);
    }
}