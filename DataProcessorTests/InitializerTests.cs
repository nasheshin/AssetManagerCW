using System;
using AssetManager.DataUtils;
using AssetManager.Models;
using NUnit.Framework;

namespace DataProcessorTests
{
    [TestFixture]
    public class InitializerTests
    {
        private readonly DataProcessorFactory _processorFactory;

        public InitializerTests()
        {
            var database = new DataContext();
            _processorFactory = new DataProcessorFactory(database);
        }
        
        [Test]
        public void InitializeDataProcessorAnalytics_Standard_ReturnDataProcessorAnalytics()
        {
            var proc = _processorFactory.CreateProcessor(DataProcessorType.AssetAnalytics);
            
            Assert.IsInstanceOf(typeof(DataProcessorAnalytics), proc);
        }
        
        [Test]
        public void InitializeDataProcessorBrokers_Standard_ReturnDataProcessorBrokers()
        {
            var proc = _processorFactory.CreateProcessor(DataProcessorType.Brokers);
            
            Assert.IsInstanceOf(typeof(DataProcessorBrokers), proc);
        }
        
        [Test]
        public void InitializeDataProcessorLogs_DatabaseHasUser_ReturnDataProcessorLogs()
        {
            var proc = _processorFactory.CreateProcessor(DataProcessorType.Logs, 1);
            
            Assert.IsInstanceOf(typeof(DataProcessorLogs), proc);
        }
        
        [Test]
        public void InitializeDataProcessorLogs_DatabaseHasNotUser_ThrowException()
        {
            try
            {
                _processorFactory.CreateProcessor(DataProcessorType.Logs, -100);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void InitializeDataProcessorNewsItems_Standard_ReturnDataProcessorNewsItems()
        {
            var proc = _processorFactory.CreateProcessor(DataProcessorType.NewsItems);
            
            Assert.IsInstanceOf(typeof(DataProcessorNewsItems), proc);
        }

        [Test]
        public void InitializeDataProcessorOperations_DatabaseHasUser_ReturnDataProcessorOperations()
        {
            var proc = _processorFactory.CreateProcessor(DataProcessorType.Operations, 1);
            
            Assert.IsInstanceOf(typeof(DataProcessorOperations), proc);
        }
        
        [Test]
        public void InitializeDataProcessorOperations_DatabaseHasNotUser_ThrowException()
        {
            try
            {
                _processorFactory.CreateProcessor(DataProcessorType.Operations, -100);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void InitializeDataProcessorPosts_DatabaseHasUser_ReturnDataProcessorPosts()
        {
            var proc = _processorFactory.CreateProcessor(DataProcessorType.Posts, 1);
            
            Assert.IsInstanceOf(typeof(DataProcessorPosts), proc);
        }
        
        [Test]
        public void InitializeDataProcessorPosts_DatabaseHasNotUser_ThrowException()
        {
            try
            {
                _processorFactory.CreateProcessor(DataProcessorType.Posts, -100);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }
        
        [Test]
        public void Initialize_DataProcessorUsers_ReturnDataProcessorUsers()
        {
            var proc = _processorFactory.CreateProcessor(DataProcessorType.Users);
            
            Assert.IsInstanceOf(typeof(DataProcessorUsers), proc);
        }
    }
}