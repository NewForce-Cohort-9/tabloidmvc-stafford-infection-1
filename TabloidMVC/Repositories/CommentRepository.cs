using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NuGet.Protocol.Plugins;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class CommentRepository: BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }


        ////COMMENTED OUT CODE: No longer want to GetAllComments, 
        //////instead want to get comments specific to a selected post

        //public List<Comment> GetAllComments()
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            //link back to post? instead of showing p.Id in SELECT
        //            cmd.CommandText = @"
        //               SELECT c.Id, c.Subject, c.Content, c.UserProfileId, c.CreateDateTime, p.Title, u.DisplayName AS Author, p.Id AS PostId, p.Title AS TitleOfPost
        //               FROM Comment c
        //                      LEFT JOIN Post p ON p.Id = c.PostId
        //                      LEFT JOIN UserProfile u ON c.UserProfileId = u.Id
        //                      ORDER BY c.CreateDateTime DESC
        //                      ";
        //            var reader = cmd.ExecuteReader();

        //            var comments = new List<Comment>();

        //            while (reader.Read())
        //            {
        //                comments.Add(NewCommentFromReader(reader));
        //            }

        //            reader.Close();

        //            return comments;
        //        }
        //    }
        //}


        public List<Comment> GetCommentsByPostId(int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
            SELECT c.Id, c.Subject, c.Content, c.UserProfileId, c.CreateDateTime, 
                   p.Id AS PostId, p.Title AS TitleOfPost, 
                   u.DisplayName AS Author
            FROM Comment c
            LEFT JOIN Post p ON p.Id = c.PostId
            LEFT JOIN UserProfile u ON c.UserProfileId = u.Id
            WHERE c.PostId = @PostId
            ORDER BY c.CreateDateTime DESC";

                    cmd.Parameters.AddWithValue("@PostId", postId);

                    var reader = cmd.ExecuteReader();
                    var comments = new List<Comment>();

                    while (reader.Read())
                    {
                        comments.Add(NewCommentFromReader(reader));
                    }

                    reader.Close();
                    return comments;
                }
            }
        }
        // For matching user's Id with comment's user profile id for edit/delete access, make sure to have  UserProfileId in GetCommentsByPostId and NewCommentFromReader! 

        public void Add(Comment comment)
        {
            try
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
            INSERT INTO Comment (
                Subject, Content, UserProfileId, CreateDateTime, PostId
            )
            OUTPUT INSERTED.ID
            VALUES (
                @Subject, @Content, @UserProfileId, @CreateDateTime, @PostId
            )";

                        cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                        cmd.Parameters.AddWithValue("@Content", comment.Content);
                        cmd.Parameters.AddWithValue("@UserProfileId", comment.UserProfileId);
                        cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);
                        cmd.Parameters.AddWithValue("@PostId", comment.PostId);



                        comment.Id = (int)cmd.ExecuteScalar();
                    }
                }
            }
                    catch (Exception ex)
                    {
                        // Log exception and/or handle error
                        throw new ApplicationException("An error occurred while adding the comment.", ex);
                    }
        }

        //added for DELETE:
        public Comment GetCommentById(int commentId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT c.Id, c.Subject, c.Content, 
                              c.CreateDateTime, c.UserProfileId,
                              c.PostId,
                              u.DisplayName AS Author,  
                              p.TItle AS TitleOfPost
                         FROM Comment c
                             
                             LEFT JOIN UserProfile u ON c.UserProfileId = u.Id
                        LEFT JOIN Post p ON c.PostId = p.Id
                             WHERE c.Id = @id";
                    //note: must add Author bcs its in NewCommentFromReader method at the bottom that is called in this GetCommentById method 
                    // WHERE CreateDateTime < SYSDATETIME()
                    cmd.Parameters.AddWithValue("@id", commentId);
                    var reader = cmd.ExecuteReader();

                    Comment comment = null;

                    if (reader.Read())
                    {
                        comment = NewCommentFromReader(reader);
                    }

                    reader.Close();

                    return comment;
                }
            }
        }


        public void Delete(int commentId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        DELETE FROM Comment
                        WHERE Id = @id
                    ";
                    cmd.Parameters.AddWithValue("@id", commentId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Edit(int id, Comment comment)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                    // Update Comment
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                    UPDATE Comment
                    SET 
                        Subject = @subject, 
                        Content = @content, 
                        PostId = @postId
                    WHERE Id = @id";
                        

                        // Add parameters to SQL command
                        cmd.Parameters.AddWithValue("@subject", (object)comment.Subject ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@content", (object)comment.Content ?? DBNull.Value);
                       cmd.Parameters.AddWithValue("@postId", comment.PostId);
                        cmd.Parameters.AddWithValue("@id", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("No rows were updated. The comment might not exist.");
                        }
                    }
            }
        }

        //this method is copied from PostRepo
        // Note: whatever properties you got in here eg. PostId and TitleOfPost you must have above in SQL Query.
        //Also, use the ALIAS name you gave to identify column by name eg. (reader.GetOrdinal("TitleOfPost")
        private Comment NewCommentFromReader(SqlDataReader reader)
        {
            return new Comment()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")), //reader.GetOrdinal("ColumnName") must match sql querry
                Subject = reader.GetString(reader.GetOrdinal("Subject")),
                Content = reader.GetString(reader.GetOrdinal("Content")),
                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                PostId = reader.GetInt32(reader.GetOrdinal("PostId")), //make sure its listed on SELECT and as PostId
                UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")), // For checking user id for edit/delete
                UserProfile = new UserProfile
                {
                    DisplayName = reader.GetString(reader.GetOrdinal("Author")),
                },
                Post = new Post
                {
                    Title = reader.GetString(reader.GetOrdinal("TitleOfPost")),
                }

            };
        }

    }
}
