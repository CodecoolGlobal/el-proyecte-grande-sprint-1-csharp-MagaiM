using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Models.Enums;
using ElProyecteGrandeSprint1.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using NUnit.Framework;

namespace TestBackend
{
    public class ArticleServiceTest: DatabaseMockInSetup 
    {
        private readonly UserServiceHelper _serviceHelper = new();
        private EmailSender _mockedEmailSender;
        private UserService _service;
        private ArticleService _articleService;

        [SetUp]
        public void Setup()
        {
            var mockedEmailSender = NSubstitute.Substitute.For<EmailSender>();
            _mockedEmailSender = mockedEmailSender;
            _service = new UserService(_context, _serviceHelper, _mockedEmailSender);
            _articleService = new ArticleService(_context, _service);

        }

        [Test]
        public async Task TestGetArticlesInArticleService()
        {
            var response = await _articleService.GetArticles();
            Assert.AreEqual(_context.Articles, response);
        }

        [Test]
        public async Task TestGetArticleInArticleService()
        {
            var response = await _articleService.GetArticle(1);
            Assert.AreEqual("This is a text", response.Title);
            Assert.AreEqual("Auto Generated Article", response.Description);
            Assert.AreEqual("Admin", response.Author.UserName);
            Assert.AreEqual(Theme.AMA, response.Theme);
            var loremIpsum = new LoremIpsum();
            Assert.AreEqual(loremIpsum.loremIpsum, response.ArticleText);
        }

        [Test]
        public async Task TestChangeArticleInArticleService()
        {
            NewArticle newArticle = new NewArticle()
            {
                Title = "valami",
                Description = "valami",
                Author = "Admin",
                Theme = Theme.AMA,
                ArticleText = "valami"
            };
            await _articleService.ChangeArticle(1, newArticle);
            var changedArticle = _articleService.GetArticle(5).Result;
            Assert.AreEqual(newArticle.Author, changedArticle.Author.UserName);
            Assert.AreEqual(newArticle.Title, changedArticle.Title);
            Assert.AreEqual(newArticle.Description, changedArticle.Description);
            Assert.AreEqual(newArticle.Theme, changedArticle.Theme);
            Assert.AreEqual(newArticle.ArticleText, changedArticle.ArticleText);
        }

        [Test]
        public async Task TestUploadArticleInArticleService()
        {
            NewArticle newArticle = new NewArticle()
            {
                Title = "valami",
                Description = "valami",
                Author = "Admin",
                Theme = Theme.AMA,
                ArticleText = "valami"
            };
            await _articleService.UploadArticle(newArticle);
            var changedArticle = _articleService.GetArticle(5).Result;
            Assert.AreEqual(newArticle.Author, changedArticle.Author.UserName);
            Assert.AreEqual(newArticle.Title, changedArticle.Title);
            Assert.AreEqual(newArticle.Description, changedArticle.Description);
            Assert.AreEqual(newArticle.Theme, changedArticle.Theme);
            Assert.AreEqual(newArticle.ArticleText, changedArticle.ArticleText);
        }
    }
}
