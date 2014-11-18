namespace PhotoDealer.Web.Tests
{
    using System;
    using System.Transactions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PhotoDealer.Data;
    using PhotoDealer.Data.Models;

    [TestClass]
    public class CategoryGroupRepositoryTests
    {
        static TransactionScope tran;

        [TestInitialize]
        public void TestInit()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void AddCategoryGroup_WhenGroupIsValid_ShouldAddToDb()
        {
            var context = new AppDbContext();
            var data = new PhotoDealerData(context);

            var categoryGroup = new CategoryGroup()
            {
                GroupName = "Test category group",
            };

            data.CategoryGroups.Add(categoryGroup);
            data.SaveChanges();

            var categoryGroupInDb = context.CategoryGroups.Find(categoryGroup.CategoryGroupId);

            Assert.IsNotNull(categoryGroupInDb);
            Assert.AreEqual(categoryGroup.GroupName, categoryGroupInDb.GroupName);
        }

        [TestMethod]
        public void FindCategoryGroup_WhenCategoryGroupInDb_ShouldReturnCategoryGroup()
        {
            var context = new AppDbContext();
            var data = new PhotoDealerData(context);

            var categoryGroup = new CategoryGroup()
            {
                GroupName = "Test category group",
            };

            context.CategoryGroups.Add(categoryGroup);
            data.SaveChanges();

            var categoryGroupInDb = data.CategoryGroups.GetById(categoryGroup.CategoryGroupId);

            Assert.IsNotNull(categoryGroupInDb);
            Assert.AreEqual(categoryGroup.GroupName, categoryGroupInDb.GroupName);
        }

        [TestMethod]
        public void DeleteCategoryGroup_WhenCategoryGroupInDb_ShouldDeletecategoryGroup()
        {
            var context = new AppDbContext();
            var data = new PhotoDealerData(context);

            var categoryGroup = new CategoryGroup()
            {
                GroupName = "Test category group",
            };

            context.CategoryGroups.Add(categoryGroup);
            data.SaveChanges();

            data.CategoryGroups.Delete(categoryGroup.CategoryGroupId);

            var categoryGroupInDb = context.CategoryGroups.Find(categoryGroup.CategoryGroupId);

            Assert.IsNotNull(categoryGroupInDb);
        }

        [TestMethod]
        public void UpdateCategoryGroupName_WhenCategoryGroupInDb_ShouldUpdateName()
        {
            var context = new AppDbContext();
            var data = new PhotoDealerData(context);

            var categoryGroup = new CategoryGroup()
            {
                GroupName = "Test category group",
            };

            string newName = "Updated Test category group";

            context.CategoryGroups.Add(categoryGroup);
            data.SaveChanges();

            var categoryGroupInDb = context.CategoryGroups.Find(categoryGroup.CategoryGroupId);
            categoryGroupInDb.GroupName = newName;

            data.CategoryGroups.Update(categoryGroupInDb);

            categoryGroupInDb = data.CategoryGroups.GetById(categoryGroup.CategoryGroupId);

            Assert.IsNotNull(categoryGroupInDb);
            Assert.AreEqual(newName, categoryGroupInDb.GroupName);
        }
    }
}
