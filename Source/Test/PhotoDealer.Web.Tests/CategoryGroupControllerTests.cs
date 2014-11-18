namespace PhotoDealer.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Telerik.JustMock;
    using PhotoDealer.Data;
    using PhotoDealer.Data.Models;
    using PhotoDealer.Web.Controllers;
    using PhotoDealer.Web.Infrastructure.Mapping;
    using PhotoDealer.Web.Infrastructure.UserProvider;
    using PhotoDealer.Web.ViewModels;

    [TestClass]
    public class CategoryGroupControllerTests
    {
        [TestInitialize]
        public void TestInit()
        {
            // var autoMapper = new AutoMapperConfig(Assembly.GetExecutingAssembly()); PhotoDealer.Web
            var autoMapper = new AutoMapperConfig(Assembly.Load("PhotoDealer.Web"));
            autoMapper.Execute();
        }

        [TestMethod]
        public void GetAll_WhenValid_ShouldReturnCategoryGroupCollection()
        {
            var categoryGroups = new List<CategoryGroup>(){
                new CategoryGroup(){ GroupName = "Test Group Name #1"},
                new CategoryGroup(){ GroupName = "Test Group Name #2"},
                new CategoryGroup(){ GroupName = "Test Group Name #3"},
            };

            var fakeData = Mock.Create<IPhotoDealerData>();
            var fakeUser = Mock.Create<IUserIdProvider>();
            var fakeImapper = Mock.Create<IMapFrom<CategoryGroup>>();

            Mock.Arrange(() => fakeData.CategoryGroups.All())
                .Returns(() => categoryGroups.AsQueryable());

            var controller = new CategoryGroupController(fakeData, fakeUser);
            var result = controller.GetAll();

            Assert.IsInstanceOfType(result, typeof(JsonResult));

            var resultData = result.Data as IEnumerable<CategoryGroupViewModel>;
            var resultList = resultData.ToList();

            Assert.AreEqual(categoryGroups.Count, resultList.Count);

            for (int i = 0; i < categoryGroups.Count; i++)
            {
                Assert.AreEqual(categoryGroups[i].GroupName, resultList[i].GroupName);
            }
        }

        [TestMethod]
        public void GetView_WhenValid_ShouldReturnCategoryGroupView()
        {
            var categoryGroups = new List<CategoryGroup>(){
                new CategoryGroup(){ GroupName = "Test Group Name #1"},
                new CategoryGroup(){ GroupName = "Test Group Name #2"},
                new CategoryGroup(){ GroupName = "Test Group Name #3"},
            };

            var fakeData = Mock.Create<IPhotoDealerData>();
            var fakeUser = Mock.Create<IUserIdProvider>();

            Mock.Arrange(() => fakeData.CategoryGroups.All())
                .Returns(() => categoryGroups.AsQueryable());

            var controller = new CategoryGroupController(fakeData, fakeUser);
            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var resultView = result as ViewResult;

            Assert.IsNotNull(resultView.ViewData.Model);

            var resultData = resultView.ViewData.Model as IEnumerable<CategoryGroupViewModel>;
            var resultList = resultData.ToList();

            Assert.AreEqual(categoryGroups.Count, resultList.Count);

            for (int i = 0; i < categoryGroups.Count; i++)
            {
                Assert.AreEqual(categoryGroups[i].GroupName, resultList[i].GroupName);
            }
        }
    }
}
