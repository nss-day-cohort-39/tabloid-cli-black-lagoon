﻿using System;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class SearchManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private BlogRepository _blogRepository;
        private PostRepository _postRepository;



        public SearchManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Search Menu");
            Console.WriteLine(" 1) Search Blogs");
            Console.WriteLine(" 2) Search Authors");
            Console.WriteLine(" 3) Search Posts");
            Console.WriteLine(" 4) Search All");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SearchBlogs();
                    return this;
                case "2":
                    SearchAuthors();
                    return this;
                case "3":
                    SearchPosts();
                    return this;
                case "4":
                    SearchAll();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void SearchAuthors()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();

            SearchResults<Author> results = _tagRepository.SearchAuthors(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }

        private void SearchBlogs()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();

            SearchResults<Blog> results = _blogRepository.SearchBlogs(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }
        private void SearchPosts()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();

            SearchResults<Post> results = _postRepository.SearchPosts(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }
        private void SearchAll()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();
            Console.WriteLine($"Search Results: \n");

            SearchResults<Blog> blogResults = _blogRepository.SearchBlogs(tagName);
            Console.WriteLine("Blogs:");

            if (blogResults.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                blogResults.DisplayAll();
            }

            SearchResults<Author> authorResults = _tagRepository.SearchAuthors(tagName);
            Console.WriteLine("Authors:");

            if (authorResults.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                authorResults.DisplayAll();
            }

            SearchResults<Post> postResults = _postRepository.SearchPosts(tagName);
            Console.WriteLine("Posts:");

            if (postResults.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                postResults.DisplayAll();
            }
        }
    }
}